using Microsoft.Extensions.Logging;

namespace Library.Service.Logging
{
    public class ConsoleLoggerFactoryProvider : ILoggerFactoryProvider
    {
        private readonly ILoggerFactory loggerFactory;

        public ConsoleLoggerFactoryProvider()
        {
            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Information);
            });
        }

        public ILogger<T> CreateLogger<T>()
        {
            return loggerFactory.CreateLogger<T>();
        }
    }
}
