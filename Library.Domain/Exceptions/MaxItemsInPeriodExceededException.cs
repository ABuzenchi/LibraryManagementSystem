namespace Library.Domain.Exceptions
{
    public class MaxItemsInPeriodExceededException : LibraryRuleExceptions
    {
        public MaxItemsInPeriodExceededException(int max, int days)
            : base($"Maximum of {max} items allowed in a period of {days} days.")
        {
        }
    }
}
