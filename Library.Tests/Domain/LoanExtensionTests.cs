using System;
using Library.Domain;
using Xunit;

namespace Library.Tests.Domain
{
    public class LoanExtensionTests
    {
        [Fact]
        public void LoanExtension_CanBeCreated_WithValidData()
        {
            var extension = new LoanExtension
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(14)
                },
                DaysExtended = 7,
                ExtensionDate = DateTime.Today
            };

            Assert.NotNull(extension.Loan);
        }

        [Fact]
        public void LoanExtension_DaysExtended_IsSetCorrectly()
        {
            var extension = new LoanExtension
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(14)
                },
                DaysExtended = 10,
                ExtensionDate = DateTime.Today
            };

            Assert.Equal(10, extension.DaysExtended);
        }

        [Fact]
        public void LoanExtension_ExtensionDate_IsSetCorrectly()
        {
            var date = DateTime.Today;

            var extension = new LoanExtension
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = date,
                    ReturnDueDate = date.AddDays(14)
                },
                DaysExtended = 7,
                ExtensionDate = date
            };

            Assert.Equal(date, extension.ExtensionDate);
        }

        [Fact]
        public void LoanExtension_Id_Defaults_To_Zero()
        {
            var extension = new LoanExtension
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(14)
                },
                DaysExtended = 7,
                ExtensionDate = DateTime.Today
            };

            Assert.Equal(0, extension.Id);
        }

        [Fact]
        public void LoanExtension_Loan_CanBeChanged()
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

            var extension = new LoanExtension
            {
                Loan = loan1,
                DaysExtended = 7,
                ExtensionDate = DateTime.Today
            };

            extension.Loan = loan2;

            Assert.Equal(loan2, extension.Loan);
        }

        [Fact]
        public void LoanExtension_DaysExtended_CanBeChanged()
        {
            var extension = new LoanExtension
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(14)
                },
                DaysExtended = 7,
                ExtensionDate = DateTime.Today
            };

            extension.DaysExtended = 14;

            Assert.Equal(14, extension.DaysExtended);
        }

        [Fact]
        public void LoanExtension_ExtensionDate_CanBeChanged()
        {
            var extension = new LoanExtension
            {
                Loan = new Loan
                {
                    Reader = new Reader { Id = 1, Name = "Ana" },
                    LoanDate = DateTime.Today,
                    ReturnDueDate = DateTime.Today.AddDays(14)
                },
                DaysExtended = 7,
                ExtensionDate = DateTime.Today
            };

            var newDate = DateTime.Today.AddDays(1);
            extension.ExtensionDate = newDate;

            Assert.Equal(newDate, extension.ExtensionDate);
        }

        [Fact]
        public void Multiple_LoanExtensions_CanReference_Same_Loan()
        {
            var loan = new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var ext1 = new LoanExtension
            {
                Loan = loan,
                DaysExtended = 7,
                ExtensionDate = DateTime.Today
            };

            var ext2 = new LoanExtension
            {
                Loan = loan,
                DaysExtended = 7,
                ExtensionDate = DateTime.Today.AddDays(1)
            };

            Assert.Equal(ext1.Loan, ext2.Loan);
        }
    }
}
