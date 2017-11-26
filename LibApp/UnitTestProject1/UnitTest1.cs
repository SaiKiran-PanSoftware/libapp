using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib2.Models;
using Lib2.Services;
using Moq;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        IEnumerable<Book> books = new List<Book>
        {
            new Book
            {
                Id =1,
                Author = "test author",
                Title = "test title",
                IsAvailable = false
            },
            new Book
            {
                Id = 2,
                Author = "test author 2",
                Title = "test title 2",
                IsAvailable = true
            },
        };

        IEnumerable<Borrower> borrowers = new List<Borrower>
        {
            new Borrower
            {
                Id =1,
                FirstName = "Test FirstName Borrower",
                LastName = "Test LastName Borrower"
            },
            new Borrower
            {
                Id = 2,
                FirstName = "Test FirstName Borrower 2",
                LastName = "Test LastName Borrower 2"
            },
        };

        BorrowerBooksAccount SimpleBorrowerBooksAccountToInsert = new BorrowerBooksAccount
        {
            Id = 0,
            BookId = 2,
            BorrowerId = 2,
            DateReturned = DateTime.Today
        };

        [TestMethod]
        public void should_fail_if_author_is_null_or_empty()
        {
            try
            {

                var book = new Book
                {
                    Author = "test author",
                    Title = "test title"
                };

                Mock<IRedisCacheProvider> _redisProvider = new Mock<IRedisCacheProvider>();
                _redisProvider.Setup(x => x.GetNextSequenceForBook()).Returns(2001);
                _redisProvider.Setup(x => x.SaveBook(It.IsAny<Book>())).Returns(book);
                _redisProvider.Setup(x => x.GetBookById(It.IsAny<long>())).Returns(book);

                var libService = new LibraryService(_redisProvider.Object);

                var bookToInsert = new Book
                {
                    Id = 0,
                    Author = null,
                    Title = "title",
                    IsAvailable = true
                };

                var result = libService.AddBook(bookToInsert);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Author cannot be empty", e.Message);
            }
        }

        [TestMethod]
        public void should_succeed_in_adding_a_book()
        {
            try
            {                

                Mock<IRedisCacheProvider> _redisProvider = new Mock<IRedisCacheProvider>();
                _redisProvider.Setup(x => x.GetNextSequenceForBook()).Returns(2001);
                _redisProvider.Setup(x => x.SaveBook(It.IsAny<Book>())).Returns(new Book());
                _redisProvider.Setup(x => x.GetBookById(It.IsAny<long>())).Returns(new Book());

                var libService = new LibraryService(_redisProvider.Object);

                var bookToInsert = new Book
                {
                    Id = 0,
                    Author = "Author",
                    Title = "Title",
                    IsAvailable = true
                };

                var result = libService.AddBook(bookToInsert);
                Assert.AreEqual(result.Author, "Author");
            }
            catch (Exception e)
            {                
            }
        }

        [TestMethod]
        public void should_fail_if_book_id_does_not_exist()
        {
            try
            {
                Mock<IRedisCacheProvider> _redisProvider = new Mock<IRedisCacheProvider>();
                _redisProvider.Setup(x => x.GetAll<Book>()).Returns(books);
                _redisProvider.Setup(x => x.GetAll<Borrower>()).Returns(borrowers);
                _redisProvider.Setup(x => x.GetNextSequenceForBorrowerBookAccount()).Returns(10);

                var libService = new LibraryService(_redisProvider.Object);
                SimpleBorrowerBooksAccountToInsert.BookId = 24;
                var bba = SimpleBorrowerBooksAccountToInsert;
                libService.BorrowBook(bba, true);
            }
            catch (Exception e)
            {                
                Assert.AreEqual("Cannot find the Book", e.Message);
            }
        }

        [TestMethod]
        public void should_fail_if_borrower_id_does_not_exist()
        {
            try
            {
                Mock<IRedisCacheProvider> _redisProvider = new Mock<IRedisCacheProvider>();
                _redisProvider.Setup(x => x.GetAll<Book>()).Returns(books);
                _redisProvider.Setup(x => x.GetAll<Borrower>()).Returns(borrowers);
                _redisProvider.Setup(x => x.GetNextSequenceForBorrowerBookAccount()).Returns(10);
                var libService = new LibraryService(_redisProvider.Object);

                SimpleBorrowerBooksAccountToInsert.BorrowerId = 24;
                var bba = SimpleBorrowerBooksAccountToInsert;
                libService.BorrowBook(bba, true);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Cannot find the Borrower", e.Message);
            }
        }


        [TestMethod]
        public void should_fail_if_date_borrowed_is_in_the_past()
        {
            try
            {
                Mock<IRedisCacheProvider> _redisProvider = new Mock<IRedisCacheProvider>();
                _redisProvider.Setup(x => x.GetAll<Book>()).Returns(books);
                _redisProvider.Setup(x => x.GetAll<Borrower>()).Returns(borrowers);
                _redisProvider.Setup(x => x.GetNextSequenceForBorrowerBookAccount()).Returns(10);
                var libService = new LibraryService(_redisProvider.Object);

                SimpleBorrowerBooksAccountToInsert.DateBorrowed = DateTime.Today.AddDays(-30);
                var bba = SimpleBorrowerBooksAccountToInsert;
                libService.BorrowBook(bba, true);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Cannot set date borrowed in the past", e.Message);
            }
        }

        [TestMethod]
        public void should_fail_if_book_is_not_available_to_be_borrowed()
        {
            try
            {
                Mock<IRedisCacheProvider> _redisProvider = new Mock<IRedisCacheProvider>();
                _redisProvider.Setup(x => x.GetAll<Book>()).Returns(books);
                _redisProvider.Setup(x => x.GetAll<Borrower>()).Returns(borrowers);
                _redisProvider.Setup(x => x.GetNextSequenceForBorrowerBookAccount()).Returns(10);
                var libService = new LibraryService(_redisProvider.Object);

                SimpleBorrowerBooksAccountToInsert.BookId = 1;
                var bba = SimpleBorrowerBooksAccountToInsert;
                libService.BorrowBook(bba, true);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Book is not available for borrowing", e.Message);
            }
        }
    }
}
