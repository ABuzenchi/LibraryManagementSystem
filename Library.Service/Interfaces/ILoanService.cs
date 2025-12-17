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
        void ValidateLoanItemLimit(IEnumerable<BookItem>items, int maxItemsPerLoan);
    }
}