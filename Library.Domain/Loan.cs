// Copyright (c) Buzenchi Andreea

using System.Dynamic;

namespace Library.Domain
{
    /// <summary>
    /// Represents a loan made by a reader.
    /// </summary>
    public class Loan
    {
        /// <summary>
        /// Gets or sets the unique identifier of the loan.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the reader who made this loan.
        /// </summary>
        required public Reader Reader { get; set; }

        /// <summary>
        /// Gets or sets the date when loan was made.
        /// </summary>
        public DateTime LoanDate { get; set; }

        /// <summary>
        /// Gets or sets the due date for returning the loaned items.
        /// </summary>
        public DateTime ReturnDueDate { get; set; }

        /// <summary>
        /// Gets or sets items included in this loan.
        /// </summary>
        public List<LoanItems> LoanItems { get; set; } = new ();
    }
}