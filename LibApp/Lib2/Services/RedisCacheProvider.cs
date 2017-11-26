using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;
using System.Web;
using Lib2.Models;

namespace Lib2.Services
{
    public class RedisCacheProvider : IRedisCacheProvider
    {
        public RedisCacheProvider()
        {            
        }

        public string GetData()
        {
            return "Got Data";
        }
        public Book SaveBook(Book book)
        {
            Book result;
            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<Book>();
                result = wrapper.Store(book);
            }
            return result;
        }

        public Borrower SaveBorrower(Borrower borrower)
        {
            Borrower result;
            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<Borrower>();
                result = wrapper.Store(borrower);
            }
            return result;
        }

        public T GetById<T>(T id)
        {
            T result = default(T);

            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<T>();

                result = wrapper.GetById(id);
            }
            return result;
        }

        public Book GetBookById(long bookId)
        {
            Book result;
            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<Book>();

                result = wrapper.GetById(bookId);
            }
            return result;
        }

        public Borrower GetBorrowerById(long borrowerId)
        {
            Borrower result;
            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<Borrower>();

                result = wrapper.GetById(borrowerId);
            }
            return result;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IEnumerable<T> result = default(IEnumerable<T>);
            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<T>();
                result = wrapper.GetAll();
            }

            return result;
        }

        public long GetNextSequenceForBook()
        {
            long result;
            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<Book>();
                result = wrapper.GetNextSequence();
            }

            return result;
        }

        public long GetNextSequenceForBorrower()
        {
            long result;
            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<Borrower>();
                result = wrapper.GetNextSequence();
            }

            return result;
        }

        public long GetNextSequenceForBorrowerBookAccount()
        {
            long result;
            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<BorrowerBooksAccount>();
                result = wrapper.GetNextSequence();
            }

            return result;
        }

        public BorrowerBooksAccount SaveBorrowerBooksAccount(BorrowerBooksAccount borrowerBooksAccount)
        {
            BorrowerBooksAccount result;
            using (RedisClient client = new RedisClient())
            {
                var wrapper = client.As<BorrowerBooksAccount>();
                result = wrapper.Store(borrowerBooksAccount);
            }
            return result;
        }
    }
}