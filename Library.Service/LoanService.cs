namespace Library.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Domain;
    using Library.Service.Interfaces;

    public class LoanService : ILoanService
    {
        public void ValidateLoanItemLimit(IEnumerable<BookItem>items, int maxItemsPerLoan)
        {
            if(items==null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if(maxItemsPerLoan<=0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxItemsPerLoan),
                    "Maximum items per loan must be greater than zero"
                );
            }

            if(items.Count()>maxItemsPerLoan)
            {
                throw new InvalidOperationException($"A loan cannot contain more than {maxItemsPerLoan} items.");
            }
        }

         public void ValidateDailyLoanLimit(Reader reader, DateTime loanDate, IEnumerable<Loan>existingLoansForReader,IEnumerable<BookItem> newLoanItems, int maxItemsPerDay)
        {
            if(reader==null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if(existingLoansForReader==null)
            {
                throw new ArgumentNullException(nameof(existingLoansForReader));
            }

            if(newLoanItems==null)
            {
                throw new ArgumentNullException(nameof(newLoanItems));
            }

            if(maxItemsPerDay<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxItemsPerDay));
            }

            var date=loanDate.Date;

            var alreadyBorrowedToday=existingLoansForReader.Where(l=>l.LoanDate.Date==date).Sum(l=>l.LoanItems.Count);
            var totalForToday=alreadyBorrowedToday+newLoanItems.Count();
            
            if(totalForToday>maxItemsPerDay)
            {
                throw new InvalidOperationException($"Daily loan limit exceeded. Maximum allowed is {maxItemsPerDay} items per day.");
            }
        }
    }
}