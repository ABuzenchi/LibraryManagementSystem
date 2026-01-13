// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a loan
    /// contains more items than permitted.
    /// </summary>
    public class LoanItemLimitExceededException : LibraryRuleExceptions
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="LoanItemLimitExceededException"/> class.
        /// </summary>
        /// <param name="maxAllowed">
        /// The maximum number of items allowed in a loan.
        /// </param>
        public LoanItemLimitExceededException(int maxAllowed)
            : base($"A loan cannot contain more than {maxAllowed} items.")
        {
        }
    }
}
