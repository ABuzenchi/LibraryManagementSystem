namespace Library.Domain.Exceptions
{
    public class MaxDomainsPerBookExceededException : LibraryRuleExceptions
    {
        public MaxDomainsPerBookExceededException(int max)
            : base($"A book cannot be assigned to more than {max} domains.")
        {
        }
    }
}
