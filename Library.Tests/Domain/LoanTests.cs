using Library.Domain;
using Xunit;
using System;

namespace Library.Tests.Domain
{
    public class LoanTests
    {
        [Fact]
        public void Loan_CanBeCreated_WithValidReader()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            Assert.NotNull(loan.Reader);
        }

        [Fact]
        public void Loan_LoanItems_IsInitialized()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            Assert.NotNull(loan.LoanItems);
        }
        [Fact]
        public void Loan_LoanDate_IsSetCorrectly()
        {
            var date = DateTime.Today;

            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = date,
                ReturnDueDate = date.AddDays(14)
            };

            Assert.Equal(date, loan.LoanDate);
        }

        [Fact]
        public void Loan_ReturnDueDate_IsSetCorrectly()
        {
            var date = DateTime.Today;
            var dueDate = date.AddDays(14);

            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = date,
                ReturnDueDate = dueDate
            };

            Assert.Equal(dueDate, loan.ReturnDueDate);
        }

        [Fact]
        public void Loan_Id_Defaults_To_Zero()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            Assert.Equal(0, loan.Id);
        }

        [Fact]
        public void Loan_LoanItems_CanBeEmpty()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            Assert.Empty(loan.LoanItems);
        }

        [Fact]
        public void Loan_LoanItems_CanAdd_Item()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            loan.LoanItems.Add(new LoanItems
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
            });

            Assert.Single(loan.LoanItems);
        }

        [Fact]
        public void Loan_LoanItems_CanAdd_Multiple_Items()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            loan.LoanItems.Add(new LoanItems
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
            });

            loan.LoanItems.Add(new LoanItems
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
            });

            Assert.Equal(2, loan.LoanItems.Count);
        }

        [Fact]
        public void Loan_ReturnDueDate_CanBe_Changed()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var newDueDate = DateTime.Today.AddDays(21);
            loan.ReturnDueDate = newDueDate;

            Assert.Equal(newDueDate, loan.ReturnDueDate);
        }


    }
}
