using __LibraryManagement.Data.BookRe;
using Library_Books_Management_System;
using Library_Books_Management_System.Service.BookSV;
using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        IBookService bookService = new BookService(new BookRepository());

        LibraryMenu menu = new LibraryMenu(bookService);

        menu.Run();
    }
}
