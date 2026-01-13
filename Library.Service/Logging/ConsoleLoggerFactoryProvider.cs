// Copyright (c) Buzenchi Andreea

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Library.Service.Logging
{
    /// <summary>
    /// Provides a factory for creating console-based loggers
    /// configured through application configuration.
    /// </summary>
    public class ConsoleLoggerFactoryProvider : ILoggerFactoryProvider
    {
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLoggerFactoryProvider"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The application configuration used to configure logging.
        /// </param>
        public ConsoleLoggerFactoryProvider(IConfiguration configuration)
        {
            this.loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConfiguration(configuration.GetSection("Logging"))
                    .AddConsole();
            });
        }

        /// <summary>
        /// Creates a logger instance for the specified type.
        /// </summary>
        /// <typeparam name="T">
        /// The type for which the logger is created.
        /// </typeparam>
        /// <returns>
        /// A configured <see cref="ILogger{T}"/> instance.
        /// </returns>
        public ILogger<T> CreateLogger<T>()
        {
            return this.loggerFactory.CreateLogger<T>();
        }
    }
}
