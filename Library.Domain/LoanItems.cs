namespace Library.Domain
{
    /// <summary>
    /// Represents a single item included in a loan
    /// </summary>
    public class LoanItems
    {
        /// <summary>
        /// The unique identifier of the loan item.
        /// </summary>
        public int Id{get;set;}

        /// <summary>
        /// The loan this item belongs to.
        /// </summary>
        public required Loan Loan{get;set;}

        /// <summary>
        /// The specific book item being borrowed.
        /// </summary>
        public required BookItem BookItem{get;set;}
    }
}