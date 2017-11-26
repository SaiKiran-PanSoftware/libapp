using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib2.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
    }
}