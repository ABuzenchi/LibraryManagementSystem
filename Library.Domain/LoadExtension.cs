namespace Library.Domain
{
    /// <summary>
    /// Represents an extension applied to a loan.
    /// </summary>
    public class LoanExtension
    {
        /// <summary>
        /// The unique identifier of the loan extension.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The loan to which this extension belongs.
        /// </summary>
        public required Loan Loan { get; set; }

        /// <summary>
        /// The number of days the loan was extended.
        /// </summary>
        public int DaysExtended { get; set; }

        /// <summary>
        /// The date when the extension was applied.
        /// </summary>
        public DateTime ExtensionDate { get; set; }
    }
}
