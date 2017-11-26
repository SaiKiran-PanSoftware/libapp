using Lib2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib2.Services
{
    public interface ILibraryService
    {
        string GetById(int id);
        Book AddBook(Book book);
        Borrower AddBorrower(Borrower borrower);
        IEnumerable<Borrower> GetAllBorrowers();
        void UpdateBook(Book book);
        void BorrowBook(BorrowerBooksAccount BorrowerBooksAccount, bool isBorrowing);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> SearchBooks(string searchText);
        IEnumerable<BorrowerBooksAccount> PendingBooks();
    }
}
