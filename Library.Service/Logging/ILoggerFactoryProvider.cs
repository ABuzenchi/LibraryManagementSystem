using Microsoft.Extensions.Logging;

namespace Library.Service.Logging
{
    public interface ILoggerFactoryProvider
    {
        ILogger<T> CreateLogger<T>();
    }
}
