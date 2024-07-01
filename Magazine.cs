using System;

namespace LibraryManagementSystem
{
    public class Magazine : LibraryItem
    {
        public int IssueNumber { get; set; }

        public override void Checkout(string memberId)
        {
            CheckoutItem(memberId);
            Console.WriteLine($"Magazine '{Title}' checked out by member ID: {memberId}");
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"Type: Magazine, Issue: {IssueNumber}");
        }
    }
}
