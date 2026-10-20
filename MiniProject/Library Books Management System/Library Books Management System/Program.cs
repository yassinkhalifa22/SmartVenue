using __LibraryManagement.Data.BookRe;
using Library_Books_Management_System;
using Library_Books_Management_System.Service.BookSV;
using Library_Books_Management_System.Service.BorrowSV;
using Library_Books_Management_System.Service.SalesSV;
using System;
using System.Diagnostics.Metrics;

class Program
{

    static void Main(string[] args)
    {
        IBookRepository bookRepository = new BookRepository();

        IBookService bookService = new BookService(bookRepository);

        BorrowManager borrowManager = new BorrowManager(bookRepository);

        SalesManager salesManager = new SalesManager(bookService);
        Inventory inventory = new Inventory(bookService);

        LibraryDataStore dataStore = new LibraryDataStore(bookRepository, bookService, borrowManager, salesManager);
        dataStore.LoadData();

        LibraryMenu menu = new LibraryMenu(bookService, borrowManager, salesManager, inventory);
        try
        {
            Console.Title = "Library Management System";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.Write("\x1b[3J");
            Console.Clear();
        }
        catch
        {
        }

        menu.Run();
        dataStore.SaveData();
    }

}
