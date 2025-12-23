namespace Library.Service.Interfaces
{
    using System.Collections.Generic;
    using Library.Domain;

    /// <summary>
    /// Service responsible for loan-related busines rules
    /// </summary>
    public interface ILoanService
    {
        /// <summary>
        /// Validates the maximum of items allowed in a single loan.
        /// </summary>
        /// <param name="items">Loan items</param>
        /// <param name="maxItemsperLoan">Maximum allowed items</param>
        void ValidateLoanItemLimit(IEnumerable<BookItem> items, int maxItemsPerLoan);

        void ValidateDailyLoanLimit(Reader reader, DateTime loanDate, IEnumerable<Loan> existingLoansForReader, IEnumerable<BookItem> newLoanItems, int maxItemsPerDay);
        void ValidateDistinctDomainsForLoan(IEnumerable<BookItem> items);
        void ValidateBookAvailabilityForLoan(Book book, IEnumerable<BookItem> allItemsForBooks, IEnumerable<BookItem> currentlyLoanedItems);
        void ValidateBookReborrowDelta(Reader reader, Book book, DateTime loanDate, IEnumerable<Loan> previousLoans, int deltaInDays);
    }
}