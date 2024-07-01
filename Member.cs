using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public class Member
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public List<BorrowHistory> BorrowHistoryList { get; private set; } = new List<BorrowHistory>();

        public void BorrowItem(LibraryItem item)
        {
            if (BorrowHistoryList.Any(b => b.Title == item.Title && b.ReturnDate == null))
            {
                throw new InvalidOperationException("Member already borrowed this item.");
            }

            item.Checkout(MemberId);
            BorrowHistoryList.Add(new BorrowHistory(MemberId, item.Title, DateTime.Now, null));
        }


        public void ReturnItem(LibraryItem item)
        {
            item.Return(MemberId);
            var history = BorrowHistoryList.FindLast(b => b.Title == item.Title && b.ReturnDate == null);
            if (history != null)
            {
                history.ReturnDate = DateTime.Now;
            }
            Console.WriteLine($"Member '{Name}' returned item '{item.Title}'");
        }

        public bool HasOverdueItems()
        {
            return BorrowHistoryList.Any(b => b.ReturnDate == null && (DateTime.Now - b.BorrowDate).Days > 30);
        }

        public void ExtendMembership()
        {
            ExpiryDate = ExpiryDate.AddMonths(1);
            Console.WriteLine($"Membership for member ID: {MemberId} extended to {ExpiryDate}");
        }

        public void Display()
        {
            Console.WriteLine($"Member ID: {MemberId}, Name: {Name}, Created: {CreatedDate}, Expires: {ExpiryDate}");
        }

        public void DisplayBorrowHistory()
        {
            Console.WriteLine($"Borrow History for Member ID: {MemberId}, Name: {Name}");
            foreach (var history in BorrowHistoryList)
            {
                Console.WriteLine($"Title: {history.Title}, Borrowed: {history.BorrowDate}, Returned: {(history.ReturnDate.HasValue ? history.ReturnDate.ToString() : "Not Returned")}");
            }
        }

        public bool IsExpired()
        {
            return DateTime.Now > ExpiryDate;
        }

        public class BorrowHistory
        {
            public string MemberId { get; set; }
            public string Title { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime? ReturnDate { get; set; }

            public BorrowHistory(string memberId, string title, DateTime borrowDate, DateTime? returnDate)
            {
                MemberId = memberId;
                Title = title;
                BorrowDate = borrowDate;
                ReturnDate = returnDate;
            }

            public int BorrowDays()
            {
                return ((ReturnDate ?? DateTime.Now) - BorrowDate).Days + 1;
            }
        }
    }
}
