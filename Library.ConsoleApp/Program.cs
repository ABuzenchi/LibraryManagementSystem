using Library.Domain;
using Library.Service;
using Library.Service.Logging;

var loggerProvider = new ConsoleLoggerFactoryProvider();

Console.WriteLine("=== LoanService tests ===");

var loanService = new LoanService(loggerProvider);

// test OK
loanService.ValidateLoanItemLimit(
    new List<BookItem>(),
    maxItemsPerLoan: 5);

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
        },
        maxItemsPerLoan: 1);
}
catch (Exception ex)
{
    Console.WriteLine($"Exception caught: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== BookDomainService tests ===");

var domainService = new BookDomainService(loggerProvider);

// test OK
domainService.ValidateMaxDomainsPerBook(
    new List<BookDomain>
    {
        new BookDomain { Id = 1, Name = "IT" },
        new BookDomain { Id = 2, Name = "Math" }
    },
    maxAllowedDomains: 3);

// test cu eroare (log + exception)
try
{
    domainService.ValidateMaxDomainsPerBook(
        new List<BookDomain>
        {
            new BookDomain { Id = 1, Name = "IT" },
            new BookDomain { Id = 2, Name = "Math" },
            new BookDomain { Id = 3, Name = "Physics" }
        },
        maxAllowedDomains: 2);
}
catch (Exception ex)
{
    Console.WriteLine($"Exception caught: {ex.Message}");
}
