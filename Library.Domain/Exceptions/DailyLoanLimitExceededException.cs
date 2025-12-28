namespace Library.Domain.Exceptions
{
    public class DailyLoanLimitExceededException : LibraryRuleExceptions
    {
        public DailyLoanLimitExceededException(int maxPerDay)
            : base($"Daily loan limit exceeded. Maximum allowed is {maxPerDay} items per day.")
        {
        }
    }
}
