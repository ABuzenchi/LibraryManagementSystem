namespace Library.Domain.Exceptions
{
    public abstract class LibraryRuleExceptions:Exception
    {
        protected LibraryRuleExceptions(string message):base(message){}
    }
}