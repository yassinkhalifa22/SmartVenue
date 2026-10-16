using System;
using System.Collections.Generic;
using System.Text;
using __LibraryManagement.Data.Models;
using Library_Books_Management_System.Service.BookSV;

namespace Library_Books_Management_System.Service.SalesSV
{
    public class Sale
    {

        public int SaleId { get; private set; }
        public int BookId { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime SaleDate { get; private set; }


        public Sale(int saleId, int bookId, int quantity, decimal totalPrice)
        {
            SaleId = saleId;
            BookId = bookId;
            Quantity = quantity;
            TotalPrice = totalPrice;
            SaleDate = DateTime.Now;
        }


    }
}
