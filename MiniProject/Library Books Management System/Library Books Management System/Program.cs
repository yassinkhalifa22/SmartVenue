using __LibraryManagement.Data.BookRe;
using Library_Books_Management_System;
using Library_Books_Management_System.Service.BookSV;
using System;
using System.Diagnostics.Metrics;
using Library_Books_Management_System.Service.BorrowSV;

class Program
{

    static void Main(string[] args)
    {
        IBookRepository bookRepository = new BookRepository();

        IBookService bookService = new BookService(bookRepository);

        BorrowManager borrowManager = new BorrowManager(bookRepository);

        LibraryMenu menu = new LibraryMenu(bookService, borrowManager);
        Console.Title = "Library Management System";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();

        menu.Run();
    }

}
