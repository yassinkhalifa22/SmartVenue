using __LibraryManagement.Data.BookRe;
using __LibraryManagement.Data.Models;
using Library_Books_Management_System.Service.BookSV;
using Library_Books_Management_System.Service.BorrowSV;
using Library_Books_Management_System.Service.SalesSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Library_Books_Management_System
{
    public class LibraryDataModel
    {
        public List<Book> Books { get; set; } = new();
        public List<Member> Members { get; set; } = new();
        public List<Borrow> Borrows { get; set; } = new();
        public List<Sale> Sales { get; set; } = new();
    }

    public class LibraryDataStore
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookService _bookService;
        private readonly BorrowManager _borrowManager;
        private readonly SalesManager _salesManager;
        private readonly string _filePath;

        public LibraryDataStore(
            IBookRepository bookRepository,
            IBookService bookService,
            BorrowManager borrowManager,
            SalesManager salesManager,
            string? customPath = null)
        {
            _bookRepository = bookRepository;
            _bookService = bookService;
            _borrowManager = borrowManager;
            _salesManager = salesManager;
            _filePath = customPath ?? ResolveDataFilePath();
        }

        public string FilePath => _filePath;

        private static string ResolveDataFilePath()
        {
            // First check if library_data.json already exists in CurrentDirectory
            string currentDirFile = Path.Combine(Directory.GetCurrentDirectory(), "library_data.json");
            if (File.Exists(currentDirFile))
            {
                return currentDirFile;
            }

            // Find project root directory (where the .csproj file is located)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo? dir = new DirectoryInfo(baseDir);
            while (dir != null)
            {
                string csprojPath = Path.Combine(dir.FullName, "Library Books Management System.csproj");
                if (File.Exists(csprojPath))
                {
                    return Path.Combine(dir.FullName, "library_data.json");
                }
                dir = dir.Parent;
            }

            return currentDirFile;
        }

        public void LoadData()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return;
                }

                string json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var data = JsonSerializer.Deserialize<LibraryDataModel>(json, options);
                if (data == null)
                {
                    return;
                }

                if (data.Books != null && data.Books.Count > 0)
                {
                    if (_bookRepository is BookRepository repo)
                    {
                        repo.LoadBooks(data.Books);
                    }
                    else
                    {
                        foreach (var book in data.Books)
                        {
                            _bookRepository.Add(book);
                        }
                    }

                    if (_bookService is BookService bs)
                    {
                        bs.SyncCounter();
                    }
                }

                if ((data.Members != null && data.Members.Count > 0) || (data.Borrows != null && data.Borrows.Count > 0))
                {
                    _borrowManager.LoadData(data.Members, data.Borrows);
                }

                if (data.Sales != null && data.Sales.Count > 0)
                {
                    _salesManager.LoadSales(data.Sales);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Warning] Failed to load library data: {ex.Message}");
            }
        }

        public void SaveData()
        {
            try
            {
                var data = new LibraryDataModel
                {
                    Books = _bookRepository.GetAll(),
                    Members = _borrowManager.GetAllMembers(),
                    Borrows = _borrowManager.GetAllBorrows(),
                    Sales = _salesManager.GetAllSales()
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(data, options);

                string? directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Warning] Failed to save library data: {ex.Message}");
            }
        }
    }
}
