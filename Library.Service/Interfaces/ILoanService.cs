// Copyright (c) Buzenchi Andreea

using System.Collections.Generic;
using Library.Domain;

namespace Library.Service.Interfaces
{
    /// <summary>
    /// Defines business rules related to library loans.
    /// </summary>
    public interface ILoanService
    {
        /// <summary>
        /// Validates the maximum number of items allowed in a single loan.
        /// </summary>
        /// <param name="items">
        /// The collection of book items included in the loan.
        /// </param>
        void ValidateLoanItemLimit(IEnumerable<BookItem> items);

         /// <summary>
        /// Validates the maximum number of items a reader can borrow in a single day.
        /// </summary>
        /// <param name="reader">
        /// The reader who performs the loan.
        /// </param>
        /// <param name="loanDate">
        /// The date of the loan.
        /// </param>
        /// <param name="existingLoansForReader">
        /// Existing loans for the reader.
        /// </param>
        /// <param name="newLoanItems">
        /// The items that are about to be loaned.
        /// </param>
        void ValidateDailyLoanLimit(Reader reader, DateTime loanDate, IEnumerable<Loan> existingLoansForReader, IEnumerable<BookItem> newLoanItems);

        /// <summary>
        /// Validates that loans containing three or more items
        /// include at least two distinct book domains.
        /// </summary>
        /// <param name="items">
        /// The collection of book items included in the loan.
        /// </param>
        void ValidateDistinctDomainsForLoan(IEnumerable<BookItem> items);

        /// <summary>
        /// Validates whether a book has enough available copies to be loaned.
        /// </summary>
        /// <param name="book">
        /// The book to be loaned.
        /// </param>
        /// <param name="allItemsForBooks">
        /// All physical copies of the book.
        /// </param>
        /// <param name="currentlyLoanedItems">
        /// Copies that are currently loaned.
        /// </param>
        void ValidateBookAvailabilityForLoan(Book book, IEnumerable<BookItem> allItemsForBooks, IEnumerable<BookItem> currentlyLoanedItems);

        /// <summary>
        /// Validates the minimum required time before a book can be borrowed again
        /// by the same reader.
        /// </summary>
        /// <param name="reader">
        /// The reader requesting the loan.
        /// </param>
        /// <param name="book">
        /// The book being loaned.
        /// </param>
        /// <param name="loanDate">
        /// The date of the new loan.
        /// </param>
        /// <param name="previousLoans">
        /// The reader's previous loans.
        /// </param>
        void ValidateBookReborrowDelta(Reader reader, Book book, DateTime loanDate, IEnumerable<Loan> previousLoans);

        /// <summary>
        /// Validates the maximum number of allowed loan extensions.
        /// </summary>
        /// <param name="loan">
        /// The loan to be extended.
        /// </param>
        /// <param name="existingExtensions">
        /// Existing extensions for the loan.
        /// </param>
        void ValidateLoanExtensionLimit(Loan loan, IEnumerable<LoanExtension>existingExtensions);

        /// <summary>
        /// Validates the maximum number of items a reader can borrow
        /// within a defined period.
        /// </summary>
        /// <param name="reader">
        /// The reader requesting the loan.
        /// </param>
        /// <param name="loanDate">
        /// The date of the loan.
        /// </param>
        /// <param name="existingLoans">
        /// Existing loans for the reader.
        /// </param>
        /// <param name="newLoanItems">
        /// The items that are about to be loaned.
        /// </param>
        void ValidateMaxItemsInPeriod(Reader reader, DateTime loanDate, IEnumerable<Loan>existingLoans, IEnumerable<BookItem>newLoanItems);
    }
}