// Copyright (c) Buzenchi Andreea

using Library.Domain;
using Library.Domain.Exceptions;
using Library.Service.Configuration;
using Library.Service.Interfaces;
using Library.Service.Logging;
using Microsoft.Extensions.Logging;

namespace Library.Service
{
    /// <summary>
    /// Provides business rule validations related to library loans.
    /// </summary>
    public class LoanService : ILoanService
    {

        private readonly ILogger<LoanService> logger;
        private readonly LibraryRulesSettings rules;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoanService"/> class.
        /// </summary>
        /// <param name="loggerProvider">
        /// The logger factory provider.
        /// </param>
        /// <param name="rules">
        /// The library business rules configuration.
        /// </param>
        public LoanService(ILoggerFactoryProvider loggerProvider, LibraryRulesSettings rules)
        {
            this.logger = loggerProvider.CreateLogger<LoanService>();
            this.rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        /// <inheritdoc />
        public void ValidateLoanItemLimit(IEnumerable<BookItem> items)
        {
            this.logger.LogInformation(
                "Validation loan item limit, MaxAllowed={MaxAllowed}",
                this.rules.MaxItemsPerLoan);

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (this.rules.MaxItemsPerLoan <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(this.rules.MaxItemsPerLoan),
                    "Maximum items per loan must be greater than zero");
            }

            var count = items.Count();

            if (count > this.rules.MaxItemsPerLoan)
            {
                this.logger.LogWarning(
                    "Loan item limit exceeded. Count={Count}, MaxAllowed={MaxAllowed}",
                    count,
                    this.rules.MaxItemsPerLoan);

                throw new LoanItemLimitExceededException(this.rules.MaxItemsPerLoan);
            }
        }

        /// <inheritdoc />s
        public void ValidateDailyLoanLimit(Reader reader, DateTime loanDate, IEnumerable<Loan> existingLoansForReader, IEnumerable<BookItem> newLoanItems)
        {
            this.logger.LogInformation("Validation daily loan limit for reader {ReaderId} on {Date}", reader?.Id, loanDate.Date);
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

            if (this.rules.MaxItemsPerDay <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.rules.MaxItemsPerDay));
            }

            if (reader.IsStaff)
            {
                this.logger.LogInformation("Reader {ReaderId} is staff. Daily loan limit ignored", reader.Id);
                return;
            }

            var date = loanDate.Date;

            var alreadyBorrowedToday =

             existingLoansForReader
             .Where(l => l.Reader.Id == reader.Id)
             .Where(l => l.LoanDate.Date == date)
             .Sum(l => l.LoanItems.Count);

            var totalForToday = alreadyBorrowedToday + newLoanItems.Count();

            if (totalForToday > this.rules.MaxItemsPerDay)
            {
                this.logger.LogWarning("Daily loan limit exceeded for reader {ReaderId}. Attempted={Total}, Limit={Limit}", reader.Id, totalForToday, rules.MaxItemsPerDay);
                throw new DailyLoanLimitExceededException(this.rules.MaxItemsPerDay);
            }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void ValidateBookAvailabilityForLoan(Book book, IEnumerable<BookItem> allItemsForBooks, IEnumerable<BookItem> currentlyLoanedItems)
        {
            this.logger.LogInformation("Validating availibility for book {BookId}", book?.Id);

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
                this.logger.LogWarning("No physical copies exist for book {BookId}", book.Id);
                throw new BookAvailabilityException("Cannot loan a book with no physical copies");
            }

            var loanableItems = allItemsForBooks.Where(i => !i.IsReadingRoomOnly).ToList();

            if (!loanableItems.Any())
            {
                this.logger.LogWarning("All copies are reading-room-only for book {BookId}", book.Id);
                throw new BookAvailabilityException("All copies of this book are restricted to the reading room");
            }

            var availableItems = loanableItems.Except(currentlyLoanedItems).Count();
            var minimumRequired = (int)Math.Ceiling(totalItems * 0.10);
            if (availableItems < minimumRequired)
            {
                this.logger.LogWarning("Not enough available copies for book {BookId}. Available={Available}, Required={Required}", book.Id, availableItems, minimumRequired);
                throw new BookAvailabilityException("Not enough available copies to allow loaning this book");
            }
        }

        /// <inheritdoc />
        public void ValidateBookReborrowDelta(Reader reader, Book book, DateTime loanDate, IEnumerable<Loan> previousLoans)
        {
            this.logger.LogInformation("Validating reborrow delta for reader {ReaderId} and book {BookId}", reader?.Id, book?.Id);
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

            if (this.rules.ReborrowDeltaDays <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.rules.ReborrowDeltaDays));
            }

            var effectiveDelta = reader.IsStaff ? Math.Max(1, this.rules.ReborrowDeltaDays / 2) : this.rules.ReborrowDeltaDays;

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
                this.logger.LogWarning("Reborrow delta violated for reader {ReaderId}, book {BookId}. DaysSienceLastLoan={Days}", reader.Id, book.Id, daysSinceLastLoan);
                throw new ReborrowDeltaException(this.rules.ReborrowDeltaDays);
            }
        }

        /// <inheritdoc />
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

            if (this.rules.MaxLoanExtensions <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.rules.MaxLoanExtensions));
            }

            var effectiveLimit = loan.Reader.IsStaff ? this.rules.MaxLoanExtensions * 2 : this.rules.MaxLoanExtensions;
            var extensionCount = existingExtensions.Count(e => e.Loan.Id == loan.Id);

            if (extensionCount >= effectiveLimit)
            {
                throw new LoanExtensionLimitExceededException(this.rules.MaxLoanExtensions);
            }
        }

        /// <inheritdoc />
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
                throw new ArgumentOutOfRangeException(nameof(this.rules.PeriodDays));
            }

            if (rules.MaxItemsInPeriod <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.rules.MaxItemsInPeriod));
            }

            var effectivePeriod = reader.IsStaff ? Math.Max(1, this.rules.PeriodDays / 2) : this.rules.PeriodDays;
            var effectiveMax = reader.IsStaff ? this.rules.MaxItemsInPeriod * 2 : this.rules.MaxItemsInPeriod;
            var fromDate = loanDate.Date.AddDays(-effectivePeriod);

            var alreadyBorrowedToday =
            existingLoans
            .Where(l => l.Reader.Id == reader.Id)
            .Where(l => l.LoanDate.Date >= fromDate && l.LoanDate.Date <= loanDate.Date)
            .Sum(l => l.LoanItems.Count);

            var totalAfterThisLoan = alreadyBorrowedToday + newLoanItems.Count();

            if (totalAfterThisLoan > effectiveMax)
            {
                throw new MaxItemsInPeriodExceededException(effectiveMax, effectivePeriod);
            }
        }
    }
}