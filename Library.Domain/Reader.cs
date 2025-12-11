namespace Library.Domain
{
    /// <summary>
    /// Represents a library reader or a staff member
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// The unique identifier of the reader
        /// </summary>
        public int Id{get;set;}

        /// <summary>
        /// The name of the reader
        /// </summary>
        public required string Name{get;set;}

        /// <summary>
        /// Phone number
        /// </summary>
        public string? Phone{get;set;}

        /// <summary>
        /// Email address
        /// </summary>
        public string? Email{get;set;}

        /// <summary>
        /// True if the reader is library staff
        /// </summary>
        public bool IsStaff{get;set;}

        
    }
}