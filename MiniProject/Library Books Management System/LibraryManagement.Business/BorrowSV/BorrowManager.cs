using __LibraryManagement.Data.BookRe;
using __LibraryManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Books_Management_System.Service.BorrowSV
{
    public class BorrowManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly List<Member> _members = new();
        private readonly List<Borrow> _borrows = new();

        private int _memberCounter = 1;
        private int _borrowCounter = 1;

        public BorrowManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public bool AddMember(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            Member member = new Member
            {
                MemberId = _memberCounter++,
                Name = name.Trim()
            };

            _members.Add(member);
            return true;
        }

        public List<Member> GetAllMembers()
        {
            return _members.ToList();
        }


        public bool BorrowBook(int memberId, int bookId, int dueDays)
        {
            if (memberId <= 0 || bookId <= 0 || dueDays <= 0)
            {
                return false;
            }

            Member? member = _members.FirstOrDefault(m => m.MemberId == memberId);

            if (member == null)
            {
                return false;
            }

            Book? book = _bookRepository.GetById(bookId);

            if (book == null || book.AvailableCopies <= 0)
            {
                return false;
            }

            Borrow borrow = new Borrow
            {
                BorrowId = _borrowCounter++,
                MemberId = memberId,  
                BookId = bookId,
                BorrowDate = DateTime.Now,  
                DueDate = DateTime.Now.AddDays(dueDays),
                IsReturned = false,
                ReturnDate = null
            };

            book.AvailableCopies--;

            bool updated = _bookRepository.Update(book);

            if (!updated)
            {
                book.AvailableCopies++;
                return false;
            }

            _borrows.Add(borrow);

            return true;
        }


        public bool ReturnBook(int borrowId)
        {
            if (borrowId <= 0)
            {
                return false;
            }

            Borrow? borrow = _borrows.FirstOrDefault(
                b => b.BorrowId == borrowId && !b.IsReturned);

            if (borrow == null)
            {
                return false;
            }

            Book? book = _bookRepository.GetById(borrow.BookId);

            if (book == null)
            {
                return false;
            }

            if (book.AvailableCopies >= book.TotalCopies)
            {
                return false;
            }

            book.AvailableCopies++;

            bool updated = _bookRepository.Update(book);

            if (!updated)
            {
                book.AvailableCopies--;
                return false;
            }

            borrow.ReturnDate = DateTime.Now;
            borrow.IsReturned = true;

            return true;
        }


        public List<Borrow> GetActiveBorrows()
        {
            return _borrows
                .Where(b => !b.IsReturned)
                .ToList();
        }


        public List<Borrow> GetOverdueBorrows()
        {
            return _borrows
               .Where(b =>
                      b.IsReturned == false &&
                      b.DueDate < DateTime.Now).ToList();
        }


        public List<Borrow> GetAllBorrows()
        {
            return _borrows.ToList();
        }


    }
}
