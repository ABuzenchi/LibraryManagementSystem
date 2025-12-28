namespace Library.Domain.Exceptions
{
    public class LoanItemLimitExceededException : LibraryRuleExceptions
    {
        public LoanItemLimitExceededException(int maxAllowed)
            : base($"A loan cannot contain more than {maxAllowed} items.")
        {
        }
    }
}
