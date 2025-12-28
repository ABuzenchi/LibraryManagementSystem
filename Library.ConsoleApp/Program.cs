using Library.Domain;
using Library.Service;
using Library.Service.Configuration;
using Library.Service.Logging;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var rules=configuration.GetSection("LibraryRules").Get<LibraryRulesSettings>();

var loggerProvider = new ConsoleLoggerFactoryProvider(configuration);

Console.WriteLine("=== LoanService tests ===");

var loanService = new LoanService(loggerProvider,rules);

// test OK
loanService.ValidateLoanItemLimit(
    new List<BookItem>());

// test cu eroare (log + exception)
try
{
    loanService.ValidateLoanItemLimit(
        new List<BookItem>
        {
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
        });
}
catch (Exception ex)
{
    Console.WriteLine($"Exception caught: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== BookDomainService tests ===");

var domainService = new BookDomainService(loggerProvider,rules);

// test OK
domainService.ValidateMaxDomainsPerBook(
    new List<BookDomain>
    {
        new BookDomain { Id = 1, Name = "IT" },
        new BookDomain { Id = 2, Name = "Math" }
    });

// test cu eroare (log + exception)
try
{
    domainService.ValidateMaxDomainsPerBook(
        new List<BookDomain>
        {
            new BookDomain { Id = 1, Name = "IT" },
            new BookDomain { Id = 2, Name = "Math" },
            new BookDomain { Id = 3, Name = "Physics" }
        });
}
catch (Exception ex)
{
    Console.WriteLine($"Exception caught: {ex.Message}");
}
