using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    public abstract class LibraryItem : ILibraryItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public int BorrowedCount { get; private set; }
        public List<BorrowHistory> BorrowHistoryList { get; private set; } = new List<BorrowHistory>();

        protected void CheckoutItem(string memberId)
        {
            if (BorrowedCount >= Quantity)
            {
                throw new InvalidOperationException("Item is already checked out.");
            }

            BorrowedCount++;
            BorrowHistoryList.Add(new BorrowHistory(memberId, DateTime.Now, null));
        }

        protected void ReturnItem(string memberId)
        {
            var history = BorrowHistoryList.FindLast(b => b.MemberId == memberId && b.ReturnDate == null);
            if (history == null)
            {
                throw new InvalidOperationException("Item is not checked out by this member.");
            }

            history.ReturnDate = DateTime.Now;
            BorrowedCount--;
        }

        public abstract void Checkout(string memberId);

        public void Return(string memberId)
        {
            ReturnItem(memberId);
            Console.WriteLine($"Item '{Title}' returned by member ID: {memberId}");
        }

        public virtual void Display()
        {
            Console.WriteLine($"Title: {Title}, Author: {Author}, Quantity: {Quantity}, Available: {Quantity - BorrowedCount}");
        }

        public bool IsAvailable()
        {
            return BorrowedCount < Quantity;
        }

        public bool IsOverdue(string memberId)
        {
            var history = BorrowHistoryList.FindLast(b => b.MemberId == memberId && b.ReturnDate == null);
            if (history != null)
            {
                return (DateTime.Now - history.BorrowDate).Days > 30; // assuming 30 days for overdue
            }
            return false;
        }
    }

    public class BorrowHistory
    {
        public string MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public BorrowHistory(string memberId, DateTime borrowDate, DateTime? returnDate)
        {
            MemberId = memberId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
        }

        public int BorrowDays()
        {
            return ((ReturnDate ?? DateTime.Now) - BorrowDate).Days + 1;
        }
    }
}
