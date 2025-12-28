namespace Library.Domain.Exceptions
{
    public class ReborrowDeltaException : LibraryRuleExceptions
    {
        public ReborrowDeltaException(int days)
            : base($"The book cannot be borrowed again within {days} days.")
        {
        }
    }
}
