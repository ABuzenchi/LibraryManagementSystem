// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
     /// <summary>
    /// Represents an exception that is thrown when a requested book
    /// is not available for borrowing.
    /// </summary>
    public class BookAvailabilityException : LibraryRuleExceptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookAvailabilityException"/> class.
        /// </summary>
        /// <param name="message">
        /// A message that describes the error.
        /// </param>
        public BookAvailabilityException(string message)
            : base(message)
        {
        }
    }
}
