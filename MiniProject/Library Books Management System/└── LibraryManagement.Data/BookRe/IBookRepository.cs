using System;
using System.Collections.Generic;
using System.Text;
using __LibraryManagement.Data.Models; 

namespace __LibraryManagement.Data.BookRe
{
    public interface IBookRepository
    {
        void Add(Book book);

        List<Book> GetAll();

        Book? GetById(int bookId);

        Book? GetByTitle(string title);

        bool Update(Book book);

        bool Delete(int bookId);
    }
}
