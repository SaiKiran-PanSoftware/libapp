using Lib2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib2.Services
{
    public interface IRedisCacheProvider
    {
        string GetData();

        long GetNextSequenceForBook();

        Book SaveBook(Book book);
        Borrower SaveBorrower(Borrower borrower);
        T GetById<T>(T id);
        IEnumerable<T> GetAll<T>();
        long GetNextSequenceForBorrower();
        Book GetBookById(long bookId);
        long GetNextSequenceForBorrowerBookAccount();
        BorrowerBooksAccount SaveBorrowerBooksAccount(BorrowerBooksAccount borrowerBooksAccount);
        Borrower GetBorrowerById(long borrowerId);
    }
}
