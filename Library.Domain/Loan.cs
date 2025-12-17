using System.Dynamic;

namespace Library.Domain
{
    /// <summary>
    /// Represents a loan made by a reader.
    /// </summary>
    public class Loan
    {
        /// <summary>
        /// The unique identifier of the loan.
        /// </summary>
        public int Id{get;set;}

        /// <summary>
        /// The reader who made this loan
        /// </summary>
        public required Reader Reader{get;set;}

        /// <summary>
        /// The date when loan was made.
        /// </summary>
        public DateTime LoanDate{get;set;}

        /// <summary>
        /// The due date for returning the loaned items
        /// </summary>
        public DateTime ReturnDueDate{get;set;}

        /// <summary>
        /// Items included in this loan
        /// </summary>
        public List<LoanItems>LoanItems{get;set;}=new();
    }
}