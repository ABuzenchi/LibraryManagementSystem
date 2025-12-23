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
    }
}
