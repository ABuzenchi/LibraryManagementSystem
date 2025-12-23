using Library.Domain;
using Xunit;

namespace Library.Tests.Domain
{
    public class LoanItemsTests
    {
        [Fact]
        public void LoanItems_BindsLoanAndBookItem()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var item = new BookItem
            {
                Edition = new Edition
                {
                    Book = new Book { Id = 1, Title = "Test" },
                    Publisher = "Pub",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 100
                }
            };

            var loanItem = new LoanItems
            {
                Loan = loan,
                BookItem = item
            };

            Assert.Equal(loan, loanItem.Loan);
            Assert.Equal(item, loanItem.BookItem);
        }
    }
}
