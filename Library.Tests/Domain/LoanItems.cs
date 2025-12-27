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

        [Fact]
        public void LoanItems_Id_Defaults_To_Zero()
        {
            var loanItem = new LoanItems
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(14)
                },
                BookItem = new BookItem
                {
                    Edition = new Edition
                    {
                        Book = new Book { Id = 1, Title = "Test" },
                        Publisher = "Pub",
                        Year = 2024,
                        EditionNumber = 1,
                        Pages = 100
                    }
                }
            };

            Assert.Equal(0, loanItem.Id);
        }

        [Fact]
        public void LoanItems_Loan_IsNotNull()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var loanItem = new LoanItems
            {
                Loan = loan,
                BookItem = new BookItem
                {
                    Edition = new Edition
                    {
                        Book = new Book { Id = 1, Title = "Test" },
                        Publisher = "Pub",
                        Year = 2024,
                        EditionNumber = 1,
                        Pages = 100
                    }
                }
            };

            Assert.NotNull(loanItem.Loan);
        }

        [Fact]
        public void LoanItems_BookItem_IsNotNull()
        {
            var loanItem = new LoanItems
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(14)
                },
                BookItem = new BookItem
                {
                    Edition = new Edition
                    {
                        Book = new Book { Id = 1, Title = "Test" },
                        Publisher = "Pub",
                        Year = 2024,
                        EditionNumber = 1,
                        Pages = 100
                    }
                }
            };

            Assert.NotNull(loanItem.BookItem);
        }

        [Fact]
        public void LoanItems_Loan_CanBeChanged()
        {
            var loan1 = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var loan2 = new Loan
            {
                Reader = new Reader { Id = 2, Name = "Ion" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(7)
            };

            var loanItem = new LoanItems
            {
                Loan = loan1,
                BookItem = new BookItem
                {
                    Edition = new Edition
                    {
                        Book = new Book { Id = 1, Title = "Test" },
                        Publisher = "Pub",
                        Year = 2024,
                        EditionNumber = 1,
                        Pages = 100
                    }
                }
            };

            loanItem.Loan = loan2;

            Assert.Equal(loan2, loanItem.Loan);
        }

        [Fact]
        public void LoanItems_BookItem_CanBeChanged()
        {
            var item1 = new BookItem
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

            var item2 = new BookItem
            {
                Edition = new Edition
                {
                    Book = new Book { Id = 2, Title = "Test2" },
                    Publisher = "Pub",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 200
                }
            };

            var loanItem = new LoanItems
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(14)
                },
                BookItem = item1
            };

            loanItem.BookItem = item2;

            Assert.Equal(item2, loanItem.BookItem);
        }

        [Fact]
        public void Multiple_LoanItems_CanReference_Same_Loan()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var loanItem1 = new LoanItems
            {
                Loan = loan,
                BookItem = new BookItem
                {
                    Edition = new Edition
                    {
                        Book = new Book { Id = 1, Title = "Test" },
                        Publisher = "Pub",
                        Year = 2024,
                        EditionNumber = 1,
                        Pages = 100
                    }
                }
            };

            var loanItem2 = new LoanItems
            {
                Loan = loan,
                BookItem = new BookItem
                {
                    Edition = new Edition
                    {
                        Book = new Book { Id = 2, Title = "Test2" },
                        Publisher = "Pub",
                        Year = 2024,
                        EditionNumber = 1,
                        Pages = 200
                    }
                }
            };

            Assert.Equal(loanItem1.Loan, loanItem2.Loan);
        }

        [Fact]
        public void Multiple_LoanItems_CanReference_Same_BookItem()
        {
            var bookItem = new BookItem
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

            var loanItem1 = new LoanItems
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(14)
                },
                BookItem = bookItem
            };

            var loanItem2 = new LoanItems
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 2, Name = "Ion" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(7)
                },
                BookItem = bookItem
            };

            Assert.Equal(loanItem1.BookItem, loanItem2.BookItem);
        }

    }
}
