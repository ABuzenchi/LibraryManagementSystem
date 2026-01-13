// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a domain hierarchy conflict
    /// is detected between two domains.
    /// </summary>
    public class DomainConflictException : LibraryRuleExceptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainConflictException"/> class.
        /// </summary>
        /// <param name="domain">
        /// The domain that causes the conflict.
        /// </param>
        /// <param name="parent">
        /// The parent domain with which the conflict occurs.
        /// </param>
        public DomainConflictException(string domain, string parent)
            : base($"Domain conflict: '{domain}' is a subdomain of '{parent}'.")
        {
        }
    }
}
