namespace Library.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Domain;
    using Library.Service.Configuration;
    using Library.Service.Interfaces;
    using Library.Service.Logging;
    using Microsoft.Extensions.Logging;

    public class LoanService : ILoanService
    {

        private readonly ILogger<LoanService>logger;
        private readonly LibraryRulesSettings rules;


        public LoanService(ILoggerFactoryProvider loggerProvider,LibraryRulesSettings rules)
        {
            logger=loggerProvider.CreateLogger<LoanService>();
            this.rules=rules ?? throw new ArgumentNullException(nameof(rules)); 
        }
        public void ValidateLoanItemLimit(IEnumerable<BookItem> items)
        {
            logger.LogInformation("Validation loan item limit, MaxAllowed={MaxAllowed}",rules.MaxItemsPerLoan);

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (items.Count() > rules.MaxItemsPerLoan)
            {
                logger.LogWarning("Loan item limit exceeded. Count={Count},MaxAllowed={MaxAllowed}",items.Count(),rules.MaxItemsPerLoan);
                throw new InvalidOperationException($"A loan cannot contain more than {rules.MaxItemsPerLoan} items.");
            }
        }

        public void ValidateDailyLoanLimit(Reader reader, DateTime loanDate, IEnumerable<Loan> existingLoansForReader, IEnumerable<BookItem> newLoanItems)
        {
            logger.LogInformation("Validation daily loan limit for reader {ReaderId} on {Date}",reader?.Id, loanDate.Date);
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (existingLoansForReader == null)
            {
                throw new ArgumentNullException(nameof(existingLoansForReader));
            }

            if (newLoanItems == null)
            {
                throw new ArgumentNullException(nameof(newLoanItems));
            }

            if (rules.MaxItemsPerDay <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rules.MaxItemsPerDay));
            }

            if (reader.IsStaff)
            {
                logger.LogInformation("Reader {ReaderId} is staff. Daily loan limit ignored",reader.Id);
                return;
            }

            var date = loanDate.Date;

            var alreadyBorrowedToday =

             existingLoansForReader
             .Where(l => l.Reader.Id == reader.Id)
             .Where(l => l.LoanDate.Date == date)
             .Sum(l => l.LoanItems.Count);

            var totalForToday = alreadyBorrowedToday + newLoanItems.Count();

            if (totalForToday > rules.MaxItemsPerDay)
            {
                logger.LogWarning("Daily loan limit exceeded for reader {ReaderId}. Attempted={Total}, Limit={Limit}",reader.Id,totalForToday,rules.MaxItemsPerDay);
                throw new InvalidOperationException($"Daily loan limit exceeded. Maximum allowed is {rules.MaxItemsPerDay} items per day.");
            }
        }

        public void ValidateDistinctDomainsForLoan(IEnumerable<BookItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var itemList = items.ToList();

            if (itemList.Count < 3)
            {
                return;
            }

            var distinctDomains =
                itemList
                    .SelectMany(i => i.Edition.Book.Domains)
                    .Select(d => d.Id)
                    .Distinct()
                    .Count();

            if (distinctDomains < 2)
            {
                throw new InvalidOperationException(
                    "When borrowing three or more items, at least two distinct domains are required.");
            }

        }

        public void ValidateBookAvailabilityForLoan(Book book, IEnumerable<BookItem> allItemsForBooks, IEnumerable<BookItem> currentlyLoanedItems)
        {
            logger.LogInformation("Validating availibility for book {BookId}",book?.Id);

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (allItemsForBooks == null)
            {
                throw new ArgumentNullException(nameof(allItemsForBooks));
            }

            if (currentlyLoanedItems == null)
            {
                throw new ArgumentNullException(nameof(currentlyLoanedItems));
            }

            var totalItems = allItemsForBooks.Count();

            if (totalItems == 0)
            {
                logger.LogWarning("No physical copies exist for book {BookId}",book.Id);
                throw new InvalidOperationException("Cannot loan a book with no psysical copies");
            }

            var loanableItems = allItemsForBooks.Where(i => !i.IsReadingRoomOnly).ToList();

            if (!loanableItems.Any())
            {
                logger.LogWarning("All copies are reading-room-only for book {BookId}",book.Id);
                throw new InvalidOperationException("All copies of this book are restricted to the reading room");
            }

            var availableItems = loanableItems.Except(currentlyLoanedItems).Count();
            var minimumRequired = (int)Math.Ceiling(totalItems * 0.10);
            if (availableItems < minimumRequired)
            {
                logger.LogWarning("Not enough available copies for book {BookId}. Available={Available}, Required={Required}",book.Id, availableItems,minimumRequired);
                throw new InvalidOperationException("Not enough available copies to allow loaning this book");
            }
        }

        public void ValidateBookReborrowDelta(Reader reader, Book book, DateTime loanDate, IEnumerable<Loan> previousLoans)
        {
            logger.LogInformation("Validating reborrow delta for reader {ReaderId} and book {BookId}",reader?.Id,book?.Id);
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (previousLoans == null)
            {
                throw new ArgumentNullException(nameof(previousLoans));
            }

            if (rules.ReborrowDeltaDays <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rules.ReborrowDeltaDays));
            }

            var effectiveDelta = reader.IsStaff ? Math.Max(1, rules.ReborrowDeltaDays / 2) : rules.ReborrowDeltaDays;

            var lastLoanDate =
              previousLoans
              .Where(l => l.Reader.Id == reader.Id)
              .Where(l => l.LoanItems.Any(li =>
              li.BookItem.Edition.Book.Id == book.Id))
              .Select(l => l.LoanDate)
              .OrderByDescending(d => d)
              .FirstOrDefault();


            if (lastLoanDate == default)
            {
                return;
            }

            var daysSinceLastLoan = (loanDate.Date - lastLoanDate.Date).TotalDays;

            if (daysSinceLastLoan < effectiveDelta)
            {
                logger.LogWarning("Reborrow delta violated for reader {ReaderId}, book {BookId}. DaysSienceLastLoan={Days}",reader.Id,book.Id,daysSinceLastLoan);
                throw new InvalidOperationException($"The book cannot be borrowed again within {rules.ReborrowDeltaDays} days.");
            }
        }

        public void ValidateLoanExtensionLimit(Loan loan, IEnumerable<LoanExtension> existingExtensions)
        {
            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan));
            }

            if (existingExtensions == null)
            {
                throw new ArgumentNullException(nameof(existingExtensions));
            }

            if (rules.MaxLoanExtensions <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rules.MaxLoanExtensions));
            }

            var effectiveLimit = loan.Reader.IsStaff ? rules.MaxLoanExtensions * 2 : rules.MaxLoanExtensions;
            var extensionCount = existingExtensions.Count(e => e.Loan.Id == loan.Id);

            if (extensionCount >= effectiveLimit)
            {
                throw new InvalidOperationException($"Loan cannot be extended more than {rules.MaxLoanExtensions} times.");
            }
        }

        public void ValidateMaxItemsInPeriod(Reader reader, DateTime loanDate, IEnumerable<Loan> existingLoans, IEnumerable<BookItem> newLoanItems)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (existingLoans == null)
            {
                throw new ArgumentNullException(nameof(existingLoans));
            }

            if (newLoanItems == null)
            {
                throw new ArgumentNullException(nameof(newLoanItems));
            }

            if (rules.PeriodDays <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rules.PeriodDays));
            }

            if (rules.MaxItemsInPeriod <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rules.MaxItemsInPeriod));
            }

            var effectivePeriod = reader.IsStaff ? Math.Max(1, rules.PeriodDays / 2) : rules.PeriodDays;
            var effectiveMax = reader.IsStaff ? rules.MaxItemsInPeriod * 2 : rules.MaxItemsInPeriod;
            var fromDate = loanDate.Date.AddDays(-effectivePeriod);

            var alreadyBorrowedToday =
            existingLoans
            .Where(l => l.Reader.Id == reader.Id)
            .Where(l => l.LoanDate.Date >= fromDate && l.LoanDate.Date <= loanDate.Date)
            .Sum(l => l.LoanItems.Count);

            var totalAfterThisLoan = alreadyBorrowedToday + newLoanItems.Count();

            if (totalAfterThisLoan > effectiveMax)
            {
                throw new InvalidOperationException($"Maximum of {effectiveMax} items allowed in a period of {effectivePeriod} days.");
            }
        }
    }
}