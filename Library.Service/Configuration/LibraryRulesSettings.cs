namespace Library.Service.Configuration
{
    public class LibraryRulesSettings
    {
        public int MaxItemsPerLoan{get;set;}
        public int MaxItemsPerDay{get;set;}
        public int ReborrowDeltaDays{get;set;}
        public int MaxLoanExtensions{get;set;}
        public int PeriodDays{get;set;}
        public int MaxItemsInPeriod{get;set;}
        public int MaxDomainsPerBook{get;set;}
    }
}