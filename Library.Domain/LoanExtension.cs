// Copyright (c) Buzenchi Andreea

namespace Library.Domain
{
    /// <summary>
    /// Represents an extension applied to a loan.
    /// </summary>
    public class LoanExtension
    {
        /// <summary>
        /// Gets or sets the unique identifier of the loan extension.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the loan to which this extension belongs.
        /// </summary>
        required public Loan Loan { get; set; }

        /// <summary>
        /// Gets or sets the number of days the loan was extended.
        /// </summary>
        public int DaysExtended { get; set; }

        /// <summary>
        /// Gets or sets the date when the extension was applied.
        /// </summary>
        public DateTime ExtensionDate { get; set; }
    }
}
