using Library.Domain;
using Library.Domain.Exceptions;
using Library.Service;
using Library.Service.Configuration;
using Library.Service.Logging;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var rules = configuration.GetSection("LibraryRules").Get<LibraryRulesSettings>() ?? throw new InvalidOperationException("LibraryRules section is missing or invalid.");

var loggerProvider = new ConsoleLoggerFactoryProvider(configuration);

var loanService = new LoanService(loggerProvider, rules);

loanService.ValidateLoanItemLimit(
    []);

try
{
    loanService.ValidateLoanItemLimit(
        [
            new BookItem
            {
                Edition = new Edition
                {
                    Book = new Book { Id = 1, Title = "Test" },
                    Publisher = "Pub",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 100
                }
            },
            new BookItem
            {
                Edition = new Edition
                {
                    Book = new Book { Id = 2, Title = "Test2" },
                    Publisher = "Pub",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 100
                }
            }
        ]);
}
catch (Exception ex)
{
    Console.WriteLine($"Exception caught: {ex.Message}");
}

var domainService = new BookDomainService(loggerProvider, rules);

domainService.ValidateMaxDomainsPerBook(
    [
        new BookDomain { Id = 1, Name = "IT" },
        new BookDomain { Id = 2, Name = "Math" }
    ]);

try
{
    domainService.ValidateMaxDomainsPerBook(
        [
            new BookDomain { Id = 1, Name = "IT" },
            new BookDomain { Id = 2, Name = "Math" },
            new BookDomain { Id = 3, Name = "Physics" }
        ]);
}
catch (LibraryRuleExceptions ex)
{
    Console.WriteLine($"Business rule violated: {ex.Message}");
}