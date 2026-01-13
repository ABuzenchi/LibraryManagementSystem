// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
    /// <summary>
    /// Serves as the base class for all business rule exceptions
    /// within the library domain.
    /// </summary>
    public abstract class LibraryRuleExceptions : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryRuleExceptions"/> class.
        /// </summary>
        /// <param name="message">
        /// A message that describes the error.
        /// </param>
        protected LibraryRuleExceptions(string message)
            : base(message)
        {
        }
    }
}