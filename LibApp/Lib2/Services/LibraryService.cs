using Lib2.Helper;
using Lib2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib2.Services
{
    public class LibraryService : ILibraryService
    {
        private IRedisCacheProvider _redisCacheProvider;
        public LibraryService(IRedisCacheProvider redisCacheProvider)
        {
            _redisCacheProvider = redisCacheProvider;
        }

        public string GetById(int id)
        {
            var result = _redisCacheProvider.GetData();
            return result;
        }

        public Book AddBook(Book book)
        {
            // Validate Book
            try
            {
                ValidateBook(book);

                long id = 0;

                var bookToInsert = new Book
                {
                    Id = _redisCacheProvider.GetNextSequenceForBook(),
                    Author = book.Author,
                    Title = book.Title,
                    IsAvailable = true // default set to true
                };

                _redisCacheProvider.SaveBook(bookToInsert);

                id = bookToInsert.Id;

                // Get the book details that has been inserted
                var bookInserted = _redisCacheProvider.GetBookById(id);
                return bookToInsert;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ValidateBook(Book book)
        {
            if (String.IsNullOrEmpty(book.Author))
                throw new Exception("Author cannot be empty");

            if (String.IsNullOrEmpty(book.Title))
                throw new Exception("Title cannot be empty");
        }

        // Add Borrower
        public Borrower AddBorrower(Borrower borrower)
        {
            try
            {
                ValidateBorrower(borrower);
                long id = 0;

                var borrowerToAdd = new Borrower
                {
                    Id = _redisCacheProvider.GetNextSequenceForBorrower(),
                    FirstName = borrower.FirstName,
                    LastName = borrower.LastName
                };

                _redisCacheProvider.SaveBorrower(borrowerToAdd);

                id = borrowerToAdd.Id;

                // Get the borrower details that has been inserted
                var borrowerInserted = _redisCacheProvider.GetBorrowerById(id);
                return borrowerInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ValidateBorrower(Borrower borrower)
        {
            if (String.IsNullOrEmpty(borrower.FirstName))
                throw new Exception("FirstName cannot be empty");

            if (String.IsNullOrEmpty(borrower.LastName))
                throw new Exception("LastName cannot be empty");
        }

        // Add Book Borrowed by the user in BookBorrowerAccount


        // Get the list of Books, with the Borrower Book Account Details 

        // Get the list of Borrowers
        public IEnumerable<Borrower> GetAllBorrowers()
        {
            var borrowers = _redisCacheProvider.GetAll<Borrower>();
            return borrowers;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var books = _redisCacheProvider.GetAll<Book>();
            return books;
        }

        // Edit a Book 
        public void UpdateBook(Book book)
        {
            try
            {
                ValidateBook(book);

                var bookToEdit = _redisCacheProvider.GetBookById(book.Id);

                bookToEdit = book.Map(bookToEdit);
                _redisCacheProvider.SaveBook(bookToEdit);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Edit a Borrower


        // Ability to borrow a book if available
        public void BorrowBook(BorrowerBooksAccount BorrowerBooksAccount, bool isBorrowing)
        {
            try
            {
                // Get the list of books                 
                var books = _redisCacheProvider.GetAll<Book>();

                var borrowers = _redisCacheProvider.GetAll<Borrower>();

                ValidateBorrowBook(BorrowerBooksAccount, books, borrowers, isBorrowing);

                // create a borrowerBookAccount for new Account to be created 
                var bbaToCreate = BorrowerBooksAccount.Map();
                if (isBorrowing)
                    bbaToCreate.Id = _redisCacheProvider.GetNextSequenceForBorrowerBookAccount();
                                
                _redisCacheProvider.SaveBorrowerBooksAccount(bbaToCreate);

                // Update the Book 
                var book = books.FirstOrDefault(x => x.Id == bbaToCreate.BookId);
                if (book == null)
                    throw new Exception("Cannot find the book");

                book.IsAvailable = false;
                _redisCacheProvider.SaveBook(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ValidateBorrowBook(BorrowerBooksAccount borrowerBooksAccount, IEnumerable<Book> books, IEnumerable<Borrower> borrowers, bool isBorrowing)
        {
            // Check whether the book exist in the books list, if not throw error Message 
            var book = books.FirstOrDefault(x => x.Id == borrowerBooksAccount.BookId);
            if (book == null)
                throw new Exception("Cannot find the Book");

            // Check whether the book is available to borrow
            if (!book.IsAvailable)
                throw new Exception("Book is not available for borrowing");

            // Check whether the borrower is there in the list, if not throw error Message 
            var borrower = borrowers.FirstOrDefault(x => x.Id == borrowerBooksAccount.BorrowerId);
            if (borrower == null)
                throw new Exception("Cannot find the Borrower");

            if (isBorrowing)
            {
                if (borrowerBooksAccount.DateBorrowed < DateTime.Today)
                    throw new Exception("Cannot set date borrowed in the past");
            }
        }

        public IEnumerable<Book> SearchBooks(string searchText)
        {
            try
            {
                var books = _redisCacheProvider.GetAll<Book>();
                if (!String.IsNullOrEmpty(searchText))
                    books = books.Where(x => x.Title.Contains(searchText)).ToList();
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<BorrowerBooksAccount> PendingBooks()
        {
            try
            {
                var borrowedBooks = _redisCacheProvider.GetAll<BorrowerBooksAccount>();
                var books = _redisCacheProvider.GetAll<Book>();
                var borrowers = _redisCacheProvider.GetAll<Borrower>();
                var date = DateTime.Today.AddDays(-7);
                var pendingBooks = borrowedBooks.Where(x => x.DateBorrowed < date && x.DateReturned == null).ToList();
                if(pendingBooks != null)
                {
                    foreach (var pendingBook in pendingBooks)
                    {
                        pendingBook.Book = books.FirstOrDefault(x => x.Id == pendingBook.BookId);
                        pendingBook.Borrower = borrowers.FirstOrDefault(x => x.Id == pendingBook.BorrowerId);
                    }
                }
                return pendingBooks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}