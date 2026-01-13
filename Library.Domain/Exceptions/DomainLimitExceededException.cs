// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when the maximum number
    /// of domains assigned to a book is exceeded.
    /// </summary
    public class DomainLimitExceededException : LibraryRuleExceptions
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="DomainLimitExceededException"/> class.
        /// </summary>
        /// <param name="maxDomains">
        /// The maximum number of domains allowed for a book.
        /// </param>
        public DomainLimitExceededException(int maxDomains)
            : base($"A book cannot be assigned to more than {maxDomains} domains.")
        {
        }
    }
}
