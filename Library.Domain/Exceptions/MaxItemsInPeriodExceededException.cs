// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when the maximum number
    /// of items allowed within a given period is exceeded.
    /// </summary>
    public class MaxItemsInPeriodExceededException : LibraryRuleExceptions
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="MaxItemsInPeriodExceededException"/> class.
        /// </summary>
        /// <param name="max">
        /// The maximum number of items allowed.
        /// </param>
        /// <param name="days">
        /// The number of days defining the period.
        /// </param>
        public MaxItemsInPeriodExceededException(int max, int days)
            : base($"Maximum of {max} items allowed in a period of {days} days.")
        {
        }
    }
}
