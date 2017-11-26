using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib2.Models
{
    public class BorrowerBooksAccount
    {
        public long Id { get; set; }
        public long BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }
        public bool IsActive { get; set; }
    }
}