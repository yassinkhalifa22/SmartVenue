using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using __LibraryManagement.Data.Models;

namespace __LibraryManagement.Data.BookRe
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        public void Add(Book book)
        {
            _books.Add(book);
        }

        public void LoadBooks(IEnumerable<Book> books)
        {
            _books.Clear();
            _books.AddRange(books);
        }

        public List<Book> GetAll()
        {
            return _books.ToList();
        }

        public Book? GetById(int bookId)
        {
            return _books.FirstOrDefault(b => b.BookId == bookId);
        }

        public Book? GetByTitle(string title)
        {
            return _books.FirstOrDefault(b => string.Equals(b.Title,title,StringComparison.OrdinalIgnoreCase));
        }

        public bool Update(Book book)
        {
            Book? existingBook = GetById(book.BookId);

            if (existingBook is null)
            {
                return false;
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Price = book.Price;
            existingBook.TotalCopies = book.TotalCopies;
            existingBook.AvailableCopies = book.AvailableCopies;

            return true;
        }

        public bool Delete(int bookId)
        {
            Book? book = GetById(bookId);

            if (book is null)
            {
                return false;
            }

            return _books.Remove(book);
        }
    }
}
