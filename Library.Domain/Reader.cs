// Copyright (c) Buzenchi Andreea

namespace Library.Domain
{
    /// <summary>
    /// Represents a library reader or a staff member.
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// Gets or sets the unique identifier of the reader.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the reader.
        /// </summary>
        required public string Name { get; set; }

        /// <summary>
        /// Gets or sets phone number.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets email address.
        /// </summary>
        public string? Email {get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether true if the reader is library staff.
        /// </summary>
        public bool IsStaff { get; set; }
    }
}