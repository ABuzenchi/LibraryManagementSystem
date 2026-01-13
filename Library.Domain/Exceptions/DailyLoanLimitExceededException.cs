// Copyright (c) Buzenchi Andreea

namespace Library.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when the maximum
    /// number of loans allowed per day is exceeded.
    /// </summary>
    public class DailyLoanLimitExceededException : LibraryRuleExceptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyLoanLimitExceededException"/> class.
        /// </summary>
        /// <param name="maxPerDay">
        /// The maximum number of items allowed to be loaned per day.
        /// </param>
        public DailyLoanLimitExceededException(int maxPerDay)
            : base($"Daily loan limit exceeded. Maximum allowed is {maxPerDay} items per day.")
        {
        }
    }
}
