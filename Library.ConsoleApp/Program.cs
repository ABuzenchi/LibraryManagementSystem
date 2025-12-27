using Library.Domain;
using Library.Service;
using Library.Service.Logging;

var loggerProvider = new ConsoleLoggerFactoryProvider();
var loanService = new LoanService(loggerProvider);

loanService.ValidateLoanItemLimit(
    new List<BookItem>(),
    maxItemsPerLoan: 5);

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
