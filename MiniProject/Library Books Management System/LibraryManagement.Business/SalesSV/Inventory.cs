using System;
using System.Collections.Generic;
using System.Text;
using __LibraryManagement.Data.Models;
using Library_Books_Management_System.Service.BookSV;

namespace Library_Books_Management_System.Service.SalesSV
{
    public class Inventory
    {
        private readonly IBookService _bookService;
        public int LowStockLimit { get; private set; }

        public Inventory(IBookService bookService)
        {
            _bookService = bookService;
            LowStockLimit = 3;
        }


        #region Restock Book
        public Book Restock(int bookId, int quantity)
        {
            Book book = _bookService.GetBookById(bookId) ?? throw new ArgumentException("Book not found.");

            if (quantity <= 0) throw new ArgumentException("Invalid quantity.");

            book.TotalCopies += quantity;
            book.AvailableCopies += quantity;
            return book;
        }

        public void RestockBook(int bookId, int quantity)
        {
            try
            {
                Book book = Restock(bookId, quantity);
                Console.WriteLine($"Restocked successfully: \"{book.Title}\" now has {book.AvailableCopies} copies available.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Display Out Of Stock Books
        public void DisplayOutOfStockBooks()
        {
            var outOfStock = _bookService.GetAllBooks()
                .Where(b => b.AvailableCopies == 0)
                .ToList();
            Console.WriteLine("===== Out of Stock Books =====");

            if (outOfStock.Count == 0)
            {
                Console.WriteLine("No out-of-stock books.");
                return;
            }

            foreach (var b in outOfStock)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine($"ID: {b.BookId}");
                Console.WriteLine($"Title: {b.Title}");
                Console.WriteLine($"Author: {b.Author}");
                Console.WriteLine("Available Copies: 0");
            }
            Console.WriteLine("------------------------------");

        }


        #endregion

        #region Display Low Stock Books
        public void DisplayLowStockBooks()
        {
            var low = _bookService.GetAllBooks()
                .Where(b => b.AvailableCopies > 0 && b.AvailableCopies <= LowStockLimit)
                .ToList();
            Console.WriteLine("===== Low Stock Books =====");

            if (low.Count == 0)
            {
                Console.WriteLine("No low stock books.");
                return;
            }

            foreach (var b in low)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine($"ID: {b.BookId}");
                Console.WriteLine($"Title: {b.Title}");
                Console.WriteLine($"Author: {b.Author}");
                Console.WriteLine($"Available Copies: {b.AvailableCopies}");
            }
            Console.WriteLine("------------------------------");
        }
        #endregion


    }
}

