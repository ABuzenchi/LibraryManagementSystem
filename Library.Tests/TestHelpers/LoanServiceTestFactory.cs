using Library.Service;
using Library.Service.Configuration;
using Library.Service.Logging;

namespace Library.Tests.TestHelpers
{
    public static class LoanServiceTestFactory
    {
        public static LoanService Create(
            int maxItemsPerLoan = 5,
            int maxItemsPerDay = 5,
            int periodDays = 7,
            int maxItemsInPeriod = 10,
            int reborrowDeltaDays = 7,
            int maxLoanExtensions = 2)
        {
            var rules = new LibraryRulesSettings
            {
                MaxItemsPerLoan = maxItemsPerLoan,
                MaxItemsPerDay = maxItemsPerDay,
                PeriodDays = periodDays,
                MaxItemsInPeriod = maxItemsInPeriod,
                ReborrowDeltaDays = reborrowDeltaDays,
                MaxLoanExtensions = maxLoanExtensions
            };

            var loggerProvider = new TestLoggerFactoryProvider();

            return new LoanService(loggerProvider, rules);
        }
    }
}
