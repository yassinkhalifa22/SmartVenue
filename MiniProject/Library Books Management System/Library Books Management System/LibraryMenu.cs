using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Books_Management_System
{
    using __LibraryManagement.Data.Models;
    using Library_Books_Management_System.Service.BookSV;
    using System;

    public class LibraryMenu
    {
        private readonly IBookService _bookService;

        public LibraryMenu(IBookService bookService)
        {
            _bookService = bookService;
        }

        private enum EnChoice
        { AddBook = 1, DisplayAllBooks, SearchBookById, SearchBookByTitle, UpdateBook, DeleteBook, Exit }

        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("========== Library Management ==========");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Display All Books");
                Console.WriteLine("3. Search Book By ID");
                Console.WriteLine("4. Search Book By Title");
                Console.WriteLine("5. Update Book");
                Console.WriteLine("6. Delete Book");
                Console.WriteLine("7. Exit");
                Console.WriteLine("========================================");

                EnChoice choice = ReadChoice();

                Console.WriteLine();

                switch (choice)
                {
                    case EnChoice.AddBook:
                        AddBook();
                        break;

                    case EnChoice.DisplayAllBooks:
                        DisplayAllBooks();
                        break;

                    case EnChoice.SearchBookById:
                        SearchBookById();
                        break;

                    case EnChoice.SearchBookByTitle:
                        SearchBookByTitle();
                        break;

                    case EnChoice.UpdateBook:
                        UpdateBook();
                        break;

                    case EnChoice.DeleteBook:
                        DeleteBook();
                        break;

                    case EnChoice.Exit:
                        isRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }

        private EnChoice ReadChoice()
        {
            Console.Write("Enter Your Choice: ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(EnChoice), choice))
                {
                    return (EnChoice)choice;
                }

                Console.Write("Invalid choice. Enter a number from 1 to 7: ");
            }
        }

        private void AddBook()
        {
            Book book = ReadBookInfo();

            bool result = _bookService.AddBook(book);

            Console.WriteLine(result? "Book added successfully.": "Failed to add book.");
        }

        private void DisplayAllBooks()
        {
            List<Book> books = _bookService.GetAllBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            foreach (Book book in books)
            {
                DisplayBookInfo(book);
            }
        }

        private void SearchBookById()
        {
            int bookId = ReadPositiveInt("Enter Book ID: ");

            Book? book = _bookService.GetBookById(bookId);

            if (book is null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            DisplayBookInfo(book);
        }

        private void SearchBookByTitle()
        {
            string title = ReadRequiredText("Enter Book Title: ");

            Book? book = _bookService.FindBookByTitle(title);

            if (book is null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            DisplayBookInfo(book);
        }

        private void UpdateBook()
        {
            int bookId = ReadPositiveInt("Enter Book ID to update: ");

            Book? existingBook = _bookService.GetBookById(bookId);

            if (existingBook is null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine("Enter the new book information:");

            Book updatedBook = ReadBookInfo();

            bool result = _bookService.UpdateBook(bookId, updatedBook);

            Console.WriteLine(result? "Book updated successfully.": "Failed to update book.");
        }

        private void DeleteBook()
        {
            int bookId = ReadPositiveInt("Enter Book ID to delete: ");

            bool result = _bookService.DeleteBook(bookId);

            Console.WriteLine(
                result? "Book deleted successfully.": "Book not found.");
        }

        private Book ReadBookInfo()
        {
            string title = ReadRequiredText("Enter Book Title: ");

            string author = ReadRequiredText("Enter Author Name: ");

            decimal price = ReadNonNegativeDecimal("Enter Book Price: ");

            int totalCopies = ReadNonNegativeInt("Enter Total Copies: ");

            return new Book
            {
                Title = title,
                Author = author,
                Price = price,
                TotalCopies = totalCopies,
                AvailableCopies = totalCopies
            };
        }

        private void DisplayBookInfo(Book book)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"ID: {book.BookId}");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Author: {book.Author}");
            Console.WriteLine($"Price: {book.Price}");
            Console.WriteLine($"Total Copies: {book.TotalCopies}");
            Console.WriteLine($"Available Copies: {book.AvailableCopies}");
            Console.WriteLine("------------------------------");
        }

        private string ReadRequiredText(string message)
        {
            Console.Write(message);

            string? input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Input cannot be empty. Try again: ");
                input = Console.ReadLine();
            }

            return input.Trim();
        }

        private int ReadPositiveInt(string message)
        {
            Console.Write(message);

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                {
                    return value;
                }

                Console.Write("Enter a valid positive number: ");
            }
        }

        private int ReadNonNegativeInt(string message)
        {
            Console.Write(message);

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value)&& value >= 0)
                {
                    return value;
                }

                Console.Write("Enter a valid non-negative number: ");
            }
        }

        private decimal ReadNonNegativeDecimal(string message)
        {
            Console.Write(message);

            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0)
                {
                    return value;
                }

                Console.Write("Enter a valid non-negative price: ");
            }
        }
    }
}
