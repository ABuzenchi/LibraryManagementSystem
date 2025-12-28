namespace Library.Domain.Exceptions
{
    public class DomainConflictException : LibraryRuleExceptions
    {
        public DomainConflictException(string domain, string parent)
            : base($"Domain conflict: '{domain}' is a subdomain of '{parent}'.")
        {
        }
    }
}
