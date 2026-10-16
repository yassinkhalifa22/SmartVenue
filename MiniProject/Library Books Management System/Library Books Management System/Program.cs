using __LibraryManagement.Data.BookRe;
using Library_Books_Management_System;
using Library_Books_Management_System.Service.BookSV;
using Library_Books_Management_System.Service.MemberSV;

using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        IBookService bookService = new BookService(new BookRepository());

        IMemberService memberService = new MemberService();

        LibraryMenu menu = new LibraryMenu(bookService, memberService);

        menu.Run();
    }
}
