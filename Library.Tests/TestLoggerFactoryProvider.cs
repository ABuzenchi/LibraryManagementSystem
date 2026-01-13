

namespace Library.Tests
{
    using Library.Service.Logging;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    public class TestLoggerFactoryProvider : ILoggerFactoryProvider
    {
        public ILogger<T> CreateLogger<T>()
        {
            // Nu scrie nimic pe consolă, dar îți permite să construiești serviciul
            return NullLogger<T>.Instance;
        }
    }
}
