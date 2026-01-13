// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a book
    /// is borrowed again before the minimum allowed time interval.
    /// </summary>
    public class ReborrowDeltaException : LibraryRuleExceptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReborrowDeltaException"/> class.
        /// </summary>
        /// <param name="days">
        /// The minimum number of days required between borrowings.
        /// </param>
        public ReborrowDeltaException(int days)
            : base($"The book cannot be borrowed again within {days} days.")
        {
        }
    }
}
