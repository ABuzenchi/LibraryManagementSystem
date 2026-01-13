// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a loan
    /// is extended more times than allowed.
    /// </summary>
    public class LoanExtensionLimitExceededException : LibraryRuleExceptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoanExtensionLimitExceededException"/> class.
        /// </summary>
        /// <param name="max">
        /// The maximum number of allowed extensions.
        /// </param>
        public LoanExtensionLimitExceededException(int max)
            : base($"Loan cannot be extended more than {max} times.")
        {
        }
    }
}
