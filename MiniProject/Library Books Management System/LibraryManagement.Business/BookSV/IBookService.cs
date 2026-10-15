using __LibraryManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Books_Management_System.Service.BookSV
{
    public interface IBookService
    {
        bool AddBook(Book book);

        List<Book> GetAllBooks();

        Book? GetBookById(int bookId);

        Book? FindBookByTitle(string title);

        bool UpdateBook(int bookId, Book updatedBook);

        bool DeleteBook(int bookId);
    }
}
