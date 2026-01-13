// Copyright (c) Buzenchi Andreea

using Microsoft.Extensions.Logging;

namespace Library.Service.Logging
{
    /// <summary>
    /// Defines a factory for creating typed logger instances.
    /// </summary>
    public interface ILoggerFactoryProvider
    {
        /// <summary>
        /// Creates a logger instance for the specified type.
        /// </summary>
        /// <typeparam name="T">
        /// The type for which the logger is created.
        /// </typeparam>
        /// <returns>
        /// A configured <see cref="ILogger{T}"/> instance.
        /// </returns>
        ILogger<T> CreateLogger<T>();
    }
}
