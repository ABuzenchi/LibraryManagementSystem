namespace Library.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Domain;
    using Library.Service.Interfaces;

    public class LoanService : ILoanService
    {
        public void ValidateLoanItemLimit(IEnumerable<BookItem> items, int maxItemsPerLoan)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (maxItemsPerLoan <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxItemsPerLoan),
                    "Maximum items per loan must be greater than zero"
                );
            }

            if (items.Count() > maxItemsPerLoan)
            {
                throw new InvalidOperationException($"A loan cannot contain more than {maxItemsPerLoan} items.");
            }
        }

        public void ValidateDailyLoanLimit(Reader reader, DateTime loanDate, IEnumerable<Loan> existingLoansForReader, IEnumerable<BookItem> newLoanItems, int maxItemsPerDay)
        {
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

            if (maxItemsPerDay <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxItemsPerDay));
            }

            var date = loanDate.Date;

            var alreadyBorrowedToday = existingLoansForReader.Where(l => l.LoanDate.Date == date).Sum(l => l.LoanItems.Count);
            var totalForToday = alreadyBorrowedToday + newLoanItems.Count();

            if (totalForToday > maxItemsPerDay)
            {
                throw new InvalidOperationException($"Daily loan limit exceeded. Maximum allowed is {maxItemsPerDay} items per day.");
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
                throw new InvalidOperationException("Cannot loan a book with no psysical copies");
            }

            var loanableItems = allItemsForBooks.Where(i => !i.IsReadingRoomOnly).ToList();

            if (!loanableItems.Any())
            {
                throw new InvalidOperationException("All copies of this book are restricted to the reading room");
            }

            var availableItems = loanableItems.Except(currentlyLoanedItems).Count();
            var minimumRequired = (int)Math.Ceiling(totalItems * 0.10);
            if (availableItems < minimumRequired)
            {
                throw new InvalidOperationException("Not enough available copies to allow loaning this book");
            }
        }

        public void ValidateBookReborrowDelta(Reader reader, Book book, DateTime loanDate, IEnumerable<Loan> previousLoans, int deltaInDays)
        {
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

            if (deltaInDays <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(deltaInDays));
            }

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

            if (daysSinceLastLoan < deltaInDays)
            {
                throw new InvalidOperationException($"The book cannot be borrowed again within {deltaInDays} days.");
            }
        }

    }
}