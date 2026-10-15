using System;
using System.Collections.Generic;
using System.Text;

namespace __LibraryManagement.Data.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int TotalCopies  { get; set; }
        public int AvailableCopies { get; set; }

    }
}
