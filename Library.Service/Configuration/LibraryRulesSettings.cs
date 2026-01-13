// Copyright (c) Buzenchi Andreea

namespace Library.Service.Configuration
{
    /// <summary>
    /// Represents configuration settings for library business rules.
    /// These values are typically loaded from application configuration.
    /// </summary>
    public class LibraryRulesSettings
    {
        /// <summary>
        /// Gets or sets the maximum number of items allowed in a single loan.
        /// </summary>
        public int MaxItemsPerLoan { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of items that can be loaned per day.
        /// </summary>
        public int MaxItemsPerDay { get; set; }

        /// <summary>
        /// Gets or sets the minimum number of days required before a book
        /// can be borrowed again.
        /// </summary>
        public int ReborrowDeltaDays { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of times a loan can be extended.
        /// </summary>
        public int MaxLoanExtensions { get; set; }

        /// <summary>
        /// Gets or sets the number of days that define the loan period window.
        /// </summary>
        public int PeriodDays { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of items allowed within a defined period.
        /// </summary>
        public int MaxItemsInPeriod { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of domains that can be assigned to a book.
        /// </summary>
        public int MaxDomainsPerBook { get; set; }
    }
}