namespace Library.Domain.Exceptions
{
    public class LoanExtensionLimitExceededException : LibraryRuleExceptions
    {
        public LoanExtensionLimitExceededException(int max)
            : base($"Loan cannot be extended more than {max} times.")
        {
        }
    }
}
