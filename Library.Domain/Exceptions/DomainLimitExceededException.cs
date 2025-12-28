namespace Library.Domain.Exceptions
{
    public class DomainLimitExceededException : LibraryRuleExceptions
    {
        public DomainLimitExceededException(int maxDomains)
            : base($"A book cannot be assigned to more than {maxDomains} domains.")
        {
        }
    }
}
