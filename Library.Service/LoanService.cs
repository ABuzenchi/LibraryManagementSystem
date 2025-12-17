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
    }
}