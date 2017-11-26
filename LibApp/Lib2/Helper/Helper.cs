using Lib2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib2.Helper
{
    public static class Helper
    {
        public static Book Map(this Book book)
        {
            if (book == null)
                return null;

            return new Book
            {
                Id = book.Id,
                Author = book.Author,
                Title = book.Title,
                IsAvailable = book.IsAvailable
            };
        }

        public static Book Map(this Book book, Book bookDb)
        {
            if (book == null)
                return null;

            if (bookDb == null)
                bookDb = new Book();


            bookDb.Id = book.Id;
            bookDb.Author = book.Author;
            bookDb.Title = book.Title;
            bookDb.IsAvailable = book.IsAvailable;

            return bookDb;
        }

        public static BorrowerBooksAccount Map(this BorrowerBooksAccount borrowerBooksAccount)
        {
            if (borrowerBooksAccount == null)
                return null;

            return new BorrowerBooksAccount
            {
                BookId = borrowerBooksAccount.BookId,
                BorrowerId = borrowerBooksAccount.BorrowerId,
                DateBorrowed = borrowerBooksAccount.DateBorrowed,
                DateReturned = borrowerBooksAccount.DateReturned
            };
        }
    }
}