using Lib2.Models;
using Lib2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lib2.Controllers
{
    public class DefaultController : ApiController
    {

        private readonly ILibraryService _libraryService;
        public DefaultController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            return new[] { _libraryService.GetById(1) };
        }

        // Book
        [HttpPost]
        public Book PostAddBook([FromBody]Book book)
        {
            try
            {
                var result = _libraryService.AddBook(book);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            try
            {
                var result = _libraryService.GetAllBooks();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IEnumerable<Borrower> GetBorrowers()
        {
            try
            {
                var result = _libraryService.GetAllBorrowers();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Borrower
        [HttpPost]
        public Borrower PostAddBorrower([FromBody]Borrower borrower)
        {
            try
            {
                var result = _libraryService.AddBorrower(borrower);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Borrower
        [HttpPost]
        public void PostBorrowBook([FromBody]BorrowerBooksAccount bba)
        {
            try
            {
                var bookBorrower = new BorrowerBooksAccount
                {
                    Id = 0,
                    BookId = bba.BookId,
                    BorrowerId = bba.BorrowerId,
                    DateBorrowed = DateTime.Now,
                    DateReturned = null
                };
                _libraryService.BorrowBook(bookBorrower, true);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IEnumerable<Book> PostSearchBooks([FromBody]Search search)
        {
            try
            {                
               var books =_libraryService.SearchBooks(search.SearchText);
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IEnumerable<BorrowerBooksAccount> GetPendingBooks()
        {
            try
            {
                var pendingBooks = _libraryService.PendingBooks();
                return pendingBooks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
