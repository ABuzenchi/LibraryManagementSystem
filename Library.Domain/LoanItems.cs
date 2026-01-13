// Copyright (c) Buzenchi Andreea

namespace Library.Domain
{
    /// <summary>
    /// Represents a single item included in a loan.
    /// </summary>
    public class LoanItems
    {
        /// <summary>
        /// Gets or sets the unique identifier of the loan item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the loan this item belongs to.
        /// </summary>
        required public Loan Loan { get; set; }

        /// <summary>
        /// Gets or sets the specific book item being borrowed.
        /// </summary>
        required public BookItem BookItem { get; set; }
    }
}