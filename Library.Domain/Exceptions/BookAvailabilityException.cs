namespace Library.Domain.Exceptions
{
    public class BookAvailabilityException : LibraryRuleExceptions
    {
        public BookAvailabilityException(string message) : base(message) { }
    }
}
