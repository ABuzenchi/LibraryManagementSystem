using Microsoft.Extensions.Logging;
using Library.Service.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Library.Tests.TestHelpers
{
    public class TestLoggerFactoryProvider : ILoggerFactoryProvider
    {
        public ILogger<T> CreateLogger<T>()
        {
            return NullLogger<T>.Instance;
        }
    }
}
