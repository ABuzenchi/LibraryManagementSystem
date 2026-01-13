using Library.Service.Logging;
using Microsoft.Extensions.Logging;
using Moq;

namespace Library.Tests.TestHelpers
{
    public class MockLoggerFactoryProvider : ILoggerFactoryProvider
    {
        public ILogger<T> CreateLogger<T>()
        {
            return new Mock<ILogger<T>>().Object;
        }
    }
}
