using System;
using System.Collections.Generic;
using System.Text;
using __LibraryManagement.Data.Models;
using Library_Books_Management_System.Service.BookSV;
namespace Library_Books_Management_System.Service.SalesSV
{
    public class SalesManager
    {
        private readonly IBookService _bookService;
        private readonly List<Sale> _sales = new();
        private int _nextId = 1;

        public SalesManager(IBookService bookService)
        {
            _bookService = bookService;
        }


        #region Sell Book
        public Sale Sell(int bookId, int quantity)
        {
            Book book = _bookService.GetBookById(bookId)
                ?? throw new ArgumentException("Book not found.");

            if (quantity <= 0)
                throw new ArgumentException("Invalid quantity.");

            if (quantity > book.AvailableCopies)
                throw new ArgumentException($"Not enough copies. Only {book.AvailableCopies} available.");

            book.AvailableCopies -= quantity;
            book.TotalCopies -= quantity;

            Sale sale = new Sale(_nextId++, bookId, quantity, book.Price * quantity);
            _sales.Add(sale);
            return sale;
        }

        public void SellBook(int bookId, int quantity)
        {
            try
            {
                Sale sale = Sell(bookId, quantity);
                Console.WriteLine($"Sale completed: {sale.Quantity} copy(ies), total price {sale.TotalPrice}.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        #endregion

        #region Display Sales History
        public void DisplaySalesHistory()
        {
            Console.WriteLine("===== Sales History =====");

            if (_sales.Count == 0)
            {
                Console.WriteLine("No sales yet.");
                return;
            }

            foreach (Sale s in _sales)
            {
                Book? book = _bookService.GetBookById(s.BookId);

                Console.WriteLine("------------------------------");
                Console.WriteLine($"Sale ID: {s.SaleId}");
                Console.WriteLine($"Book: {(book != null ? book.Title : "(deleted book)")}");
                Console.WriteLine($"Quantity: {s.Quantity}");
                Console.WriteLine($"Total Price: {s.TotalPrice}");
                Console.WriteLine($"Date: {s.SaleDate:yyyy-MM-dd HH:mm}");
            }
            Console.WriteLine("------------------------------");
        }
        #endregion

        #region Total Sales
        public decimal GetTotalSales()
        {
            return _sales.Sum(s => s.TotalPrice);
        }
        #endregion
    }
}
