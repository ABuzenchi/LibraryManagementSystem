// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a book
    /// exceeds the maximum allowed number of domains.
    /// </summary>
    public class MaxDomainsPerBookExceededException : LibraryRuleExceptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxDomainsPerBookExceededException"/> class.
        /// </summary>
        /// <param name="max">
        /// The maximum number of domains allowed per book.
        /// </param>
        public MaxDomainsPerBookExceededException(int max)
            : base($"A book cannot be assigned to more than {max} domains.")
        {
        }
    }
}
