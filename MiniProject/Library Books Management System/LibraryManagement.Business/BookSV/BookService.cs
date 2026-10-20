using __LibraryManagement.Data.BookRe;
using __LibraryManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace Library_Books_Management_System.Service.BookSV
{
    public class BookService : IBookService
    {
        private int counter = 1;
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void SyncCounter()
        {
            var books = _bookRepository.GetAll();
            if (books.Count > 0)
            {
                int maxId = books.Max(b => b.BookId);
                if (counter <= maxId)
                {
                    counter = maxId + 1;
                }
            }
        }

        public bool AddBook(Book book)
        {
            if (book == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(book.Title) ||string.IsNullOrWhiteSpace(book.Author))
            {
                return false;
            }

            if (book.Price < 0 || book.TotalCopies < 0)
            {
                return false;
            }

            SyncCounter();
            book.BookId = counter++;
            book.AvailableCopies = book.TotalCopies;

            _bookRepository.Add(book);

            return true;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book? GetBookById(int bookId)
        {
            if (bookId <= 0)
            {
                return null;
            }

            return _bookRepository.GetById(bookId);
        }

        public Book? FindBookByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return null;
            }

            return _bookRepository.GetByTitle(title.Trim());
        }

        public bool UpdateBook(int bookId, Book updatedBook)
        {
            if (bookId <= 0 || updatedBook == null)
            {
                return false;
            }

            Book? existingBook = _bookRepository.GetById(bookId);

            if (existingBook == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(updatedBook.Title) ||string.IsNullOrWhiteSpace(updatedBook.Author))
            {
                return false;
            }

            if (updatedBook.Price < 0 || updatedBook.TotalCopies < 0)
            {
                return false;
            }

            int borrowedCopies = existingBook.TotalCopies - existingBook.AvailableCopies;

            if (updatedBook.TotalCopies < borrowedCopies)
            {
                return false;
            }

            existingBook.Title = updatedBook.Title.Trim();
            existingBook.Author = updatedBook.Author.Trim();
            existingBook.Price = updatedBook.Price;
            existingBook.TotalCopies = updatedBook.TotalCopies;

            existingBook.AvailableCopies = updatedBook.TotalCopies - borrowedCopies;

            return _bookRepository.Update(existingBook);
        }

        public bool DeleteBook(int bookId)
        {
            if (bookId <= 0)
            {
                return false;
            }

            Book? book = _bookRepository.GetById(bookId);

            if (book == null)
            {
                return false;
            }

            return _bookRepository.Delete(bookId);
        }
    }
}
