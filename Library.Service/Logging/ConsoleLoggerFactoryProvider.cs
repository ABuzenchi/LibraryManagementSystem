using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Library.Service.Logging
{
    public class ConsoleLoggerFactoryProvider : ILoggerFactoryProvider
    {
        private readonly ILoggerFactory loggerFactory;

        public ConsoleLoggerFactoryProvider(IConfiguration configuration)
        {
            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConfiguration(configuration.GetSection("Logging"))
                    .AddConsole();
            });
        }

        public ILogger<T> CreateLogger<T>()
        {
            return loggerFactory.CreateLogger<T>();
        }
    }
}
