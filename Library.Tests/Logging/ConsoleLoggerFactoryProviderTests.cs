// Copyright (c) Buzenchi Andreea

using Library.Service.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Library.Tests.Logging
{
    public class ConsoleLoggerFactoryProviderTests
    {
        [Fact]
        public void Constructor_DoesNotThrow_WhenConfigurationIsValid()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            var exception = Record.Exception(() =>
                new ConsoleLoggerFactoryProvider(configuration));

            Assert.Null(exception);
        }

        [Fact]
        public void CreateLogger_ReturnsLoggerInstance()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            var provider = new ConsoleLoggerFactoryProvider(configuration);

            var logger = provider.CreateLogger<ConsoleLoggerFactoryProviderTests>();

            Assert.NotNull(logger);
            Assert.IsAssignableFrom<ILogger<ConsoleLoggerFactoryProviderTests>>(logger);
        }
    }
}
