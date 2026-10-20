using System;
using System.Collections.Generic;
using System.Text;
using __LibraryManagement.Data.Models;
using Library_Books_Management_System.Service.BookSV;

namespace Library_Books_Management_System.Service.SalesSV
{
    public class Sale
    {

        public int SaleId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }

        public Sale()
        {
            SaleDate = DateTime.Now;
        }

        public Sale(int saleId, int bookId, int quantity, decimal totalPrice)
        {
            SaleId = saleId;
            BookId = bookId;
            Quantity = quantity;
            TotalPrice = totalPrice;
            SaleDate = DateTime.Now;
        }

        public Sale(int saleId, int bookId, int quantity, decimal totalPrice, DateTime saleDate)
        {
            SaleId = saleId;
            BookId = bookId;
            Quantity = quantity;
            TotalPrice = totalPrice;
            SaleDate = saleDate;
        }


    }
}
