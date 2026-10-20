using __LibraryManagement.Data.Models;
using Library_Books_Management_System.Service.BookSV;
using Library_Books_Management_System.Service.BorrowSV;
using Library_Books_Management_System.Service.SalesSV;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Books_Management_System
{


    public class LibraryMenu
    {
        private readonly IBookService _bookService;
        private readonly BorrowManager _borrowManager;
        private readonly SalesManager _salesManager;
        private readonly Inventory _inventory;
        private readonly LibraryDataStore? _dataStore;

        public LibraryMenu(IBookService bookService, BorrowManager borrowManager, SalesManager salesManager, Inventory inventory, LibraryDataStore? dataStore = null)
        {
            _bookService = bookService;
            _borrowManager = borrowManager;
            _salesManager = salesManager;
            _inventory = inventory;
            _dataStore = dataStore;
        }

        private enum EnChoice
        {
            AddBook = 1, DisplayAllBooks, SearchBookById, SearchBookByTitle,
            UpdateBook, DeleteBook, AddMember,
            BorrowBook, ReturnBook, DisplayAllMembers,
            DisplayActiveBorrows, DisplayOverdueBorrows,
            SellBook, DisplaySalesHistory, DisplayLowStock, DisplayOutOfStock,
            RestockBook, DisplayLibrarySummary,
            Exit
        }
        private void DisplayHeader()
        {
           // Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("==================================================");
            Console.WriteLine("           LIBRARY MANAGEMENT SYSTEM");
            Console.WriteLine("==================================================");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("          Books | Members | Borrowing");
            Console.WriteLine();

            Console.ResetColor();
        }

        private void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  BOOK MANAGEMENT");
            Console.ResetColor();

            Console.WriteLine("  [1]  Add Book");
            Console.WriteLine("  [2]  Display All Books");
            Console.WriteLine("  [3]  Search Book By ID");
            Console.WriteLine("  [4]  Search Book By Title");
            Console.WriteLine("  [5]  Update Book");
            Console.WriteLine("  [6]  Delete Book");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  MEMBER & BORROWING MANAGEMENT");
            Console.ResetColor();

            Console.WriteLine("  [7]  Add Member");
            Console.WriteLine("  [8]  Borrow Book");
            Console.WriteLine("  [9]  Return Book");
            Console.WriteLine("  [10] Display All Members");
            Console.WriteLine("  [11] Display Active Borrows");
            Console.WriteLine("  [12] Display Overdue Borrows");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  SALES & INVENTORY MANAGEMENT");
            Console.ResetColor();

            Console.WriteLine("  [13] Sell Book");
            Console.WriteLine("  [14] Display Sales History");
            Console.WriteLine("  [15] Display Low Stock Books");
            Console.WriteLine("  [16] Display Out of Stock Books");
            Console.WriteLine("  [17] Restock Book");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  SYSTEM REPORTS & SUMMARY");
            Console.ResetColor();

            Console.WriteLine("  [18] Display Comprehensive Library Summary");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [19] Exit");
            Console.ResetColor();

            Console.WriteLine("==================================================");
        }

        private void ClearScreen()
        {
            try
            {
                Console.Clear();
                Console.Write("\x1b[3J");
                Console.Clear();
                if (OperatingSystem.IsWindows() && !Console.IsOutputRedirected && Console.WindowTop > 0)
                {
                    Console.SetWindowPosition(0, 0);
                }
            }
            catch
            {
            }
        }

        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                ClearScreen();
                DisplayHeader();
                DisplayMenu();

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
                    case EnChoice.AddMember:
                        AddMember();
                        break;
                    case EnChoice.BorrowBook:
                        BorrowBook();
                        break;

                    case EnChoice.ReturnBook:
                        ReturnBook();
                        break;

                    case EnChoice.DisplayAllMembers:
                        DisplayAllMembers();
                        break;

                    case EnChoice.DisplayActiveBorrows:
                        DisplayActiveBorrows();
                        break;

                    case EnChoice.DisplayOverdueBorrows:
                        DisplayOverdueBorrows();
                        break;
                    case EnChoice.SellBook:
                        SellBook();
                        break;

                    case EnChoice.DisplaySalesHistory:
                        _salesManager.DisplaySalesHistory();
                        break;

                    case EnChoice.DisplayLowStock:
                        _inventory.DisplayLowStockBooks();
                        break;

                    case EnChoice.DisplayOutOfStock:
                        _inventory.DisplayOutOfStockBooks();
                        break;

                    case EnChoice.RestockBook:
                        RestockBook();
                        break;
                    case EnChoice.DisplayLibrarySummary:
                        ShowLibrarySummary();
                        break;

                    case EnChoice.Exit:
                        isRunning = false;
                        _dataStore?.SaveData();
                        Console.WriteLine("Goodbye!");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey(true);

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

                Console.Write("Invalid choice. Enter a number from 1 to 19: ");
            }
        }


        private void AddBook()
        {
            Book book = ReadBookInfo();

            bool result = _bookService.AddBook(book);

            Console.WriteLine(result ? "Book added successfully." : "Failed to add book.");
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

            Console.WriteLine(result ? "Book updated successfully." : "Failed to update book.");
        }

        private void DeleteBook()
        {
            int bookId = ReadPositiveInt("Enter Book ID to delete: ");

            bool result = _bookService.DeleteBook(bookId);

            Console.WriteLine(
                result ? "Book deleted successfully." : "Book not found.");
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
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
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

        private void BorrowBook()
        {
            Console.Write("Enter Member ID: ");
            int memberId = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Book ID: ");
            int bookId = int.Parse(Console.ReadLine()!);

            Console.Write("Enter borrowing duration in days: ");
            int dueDays = int.Parse(Console.ReadLine()!);

            bool result = _borrowManager.BorrowBook(
                memberId,
                bookId,
                dueDays);

            if (result)
            {
                Console.WriteLine("Book borrowed successfully!");
            }
            else
            {
                Console.WriteLine(
                    "Borrowing failed. Check Member ID, Book ID, and available copies.");
            }
        }

        private void ReturnBook()
        {
            Console.Write("Enter Borrow ID: ");
            int borrowId = int.Parse(Console.ReadLine()!);

            bool result = _borrowManager.ReturnBook(borrowId);

            if (result)
            {
                Console.WriteLine("Book returned successfully!");
            }
            else
            {
                Console.WriteLine(
                    "Return failed. Please check the Borrow ID.");
            }
        }
        private void AddMember()
        {
            Console.Write("Enter Member Name: ");
            string name = Console.ReadLine() ?? "";

            bool result = _borrowManager.AddMember(name);

            if (result)
            {
                Console.WriteLine("Member added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid member name.");
            }
        }
        private void DisplayAllMembers()
        {
            List<Member> members = _borrowManager.GetAllMembers();

            if (members.Count == 0)
            {
                Console.WriteLine("No members found.");
                return;
            }

            Console.WriteLine("========== Members ==========");

            foreach (Member member in members)
            {
                Console.WriteLine(
                    $"Member ID: {member.MemberId}, Name: {member.Name}");
            }
        }
        private void DisplayActiveBorrows()
        {
            List<Borrow> borrows = _borrowManager.GetActiveBorrows();

            if (borrows.Count == 0)
            {
                Console.WriteLine("No active borrows found.");
                return;
            }

            Console.WriteLine("========== Active Borrows ==========");

            foreach (Borrow borrow in borrows)
            {
                Console.WriteLine(
                    $"Borrow ID: {borrow.BorrowId}, " +
                    $"Member ID: {borrow.MemberId}, " +
                    $"Book ID: {borrow.BookId}, " +
                    $"Borrow Date: {borrow.BorrowDate:g}, " +
                    $"Due Date: {borrow.DueDate:g}");
            }
        }
        private void DisplayOverdueBorrows()
        {
            List<Borrow> borrows = _borrowManager.GetOverdueBorrows();

            if (borrows.Count == 0)
            {
                Console.WriteLine("No overdue borrows found.");
                return;
            }

            Console.WriteLine("========== Overdue Borrows ==========");

            foreach (Borrow borrow in borrows)
            {
                Console.WriteLine(
                    $"Borrow ID: {borrow.BorrowId}, " +
                    $"Member ID: {borrow.MemberId}, " +
                    $"Book ID: {borrow.BookId}, " +
                    $"Due Date: {borrow.DueDate:g}");
            }

        }
        private void SellBook()
        {
            int bookId = ReadPositiveInt("Enter Book ID to sell: ");
            int quantity = ReadPositiveInt("Enter Quantity to sell: ");
            _salesManager.SellBook(bookId, quantity);
        }

        private void RestockBook()
        {
            int bookId = ReadPositiveInt("Enter Book ID to restock: ");
            int quantity = ReadPositiveInt("Enter Quantity to add: ");
            _inventory.RestockBook(bookId, quantity);
        }

private void ShowLibrarySummary()
        {
           
            ClearScreen();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================================================================");
            Console.WriteLine("                 COMPREHENSIVE LIBRARY SUMMARY REPORT                 ");
            Console.WriteLine("======================================================================");
            Console.ResetColor();

            // 1. حسابات الكتب والمخزون
            var books = _bookService.GetAllBooks();
            int totalBooks = books.Count;
            int totalCopiesOwned = 0;
            int totalCopiesAvailable = 0;
            decimal totalInventoryValue = 0;

            foreach (var b in books)
            {
                totalCopiesOwned += b.TotalCopies;
                totalCopiesAvailable += b.AvailableCopies;
                totalInventoryValue += (b.Price * b.TotalCopies);
            }

            var lowStockBooks = books.Where(b => b.AvailableCopies > 0 && b.AvailableCopies <= _inventory.LowStockLimit).ToList();
            var outOfStockBooks = books.Where(b => b.AvailableCopies == 0).ToList();

            // 2. حسابات الأعضاء والاستعارات
            var members = _borrowManager.GetAllMembers();
            var activeBorrows = _borrowManager.GetActiveBorrows();
            var overdueBorrows = _borrowManager.GetOverdueBorrows();

            // 3. حسابات المبيعات والماليات
            decimal totalRevenue = _salesManager.GetTotalSales();

            // --- عرض 1: الكتب والمخزون ---
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ 1. BOOKS & INVENTORY STATS ]");
            Console.ResetColor();
            Console.WriteLine(" ------------------------------------------------------------------");
            Console.WriteLine($"  • Total Book Titles        : {totalBooks}");
            Console.WriteLine($"  • Total Copies Owned       : {totalCopiesOwned}");
            Console.WriteLine($"  • Total Copies Available   : {totalCopiesAvailable}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  • Total Inventory Value    : {totalInventoryValue:C}");
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ---> Detailed Book Breakdown:");
            Console.ResetColor();

            if (books.Count == 0)
            {
                Console.WriteLine("        (No books registered in the system yet)");
            }
            else
            {
                foreach (var b in books)
                {
                    int borrowedCount = b.TotalCopies - b.AvailableCopies;
                    Console.WriteLine($"        [ID: {b.BookId}] \"{b.Title}\" by {b.Author}");
                    Console.WriteLine($"             Owned: {b.TotalCopies} | Available: {b.AvailableCopies} | Borrowed: {borrowedCount}");
                }
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ---> Inventory Status Alerts:");
            Console.ResetColor();
            Console.WriteLine($"        • Out of Stock Titles : {outOfStockBooks.Count}");
            Console.WriteLine($"        • Low Stock Titles    : {lowStockBooks.Count}");

            if (lowStockBooks.Count > 0)
            {
                foreach (var lsb in lowStockBooks)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"          ! Low Stock Alert: \"{lsb.Title}\" ({lsb.AvailableCopies} copies left)");
                    Console.ResetColor();
                }
            }

            Console.WriteLine();

            // --- عرض 2: الاستعارات والأعضاء ---
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ 2. BORROWING & MEMBERS STATS ]");
            Console.ResetColor();
            Console.WriteLine(" ------------------------------------------------------------------");
            Console.WriteLine($"  • Total Registered Members : {members.Count}");
            Console.WriteLine($"  • Active Borrowings        : {activeBorrows.Count}");

            Console.ForegroundColor = overdueBorrows.Count > 0 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine($"  • Overdue Borrowings       : {overdueBorrows.Count}");
            Console.ResetColor();

Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   ---> Active Borrowing Details:");
            Console.ResetColor();

            if (activeBorrows.Count == 0)
            {
                Console.WriteLine("        (No active borrowings at the moment)");
            }
            else
            {
                foreach (var borrow in activeBorrows)
                {
                    var book = _bookService.GetBookById(borrow.BookId);
                    string bookTitle = book != null ? book.Title : $"Book #{borrow.BookId}";
                    Console.WriteLine($"        * Member ID: {borrow.MemberId} | Book: \"{bookTitle}\" | Due: {borrow.DueDate:yyyy-MM-dd}");
                }
            }

            Console.WriteLine();

            // --- عرض 3: المبيعات والماليات ---
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ 3. SALES & FINANCIAL STATS ]");
            Console.ResetColor();
            Console.WriteLine(" ------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  • Total Revenue Generated  : {totalRevenue:C}");
            Console.ResetColor();

            Console.WriteLine("======================================================================");
            Console.WriteLine("Press any key to return to main menu...");
        }


    }
}
