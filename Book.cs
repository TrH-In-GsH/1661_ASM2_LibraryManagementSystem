using System;

namespace LibraryManagementSystem
{
    public class Book : LibraryItem
    {
        public int NumberOfPages { get; set; }

        public override void Checkout(string memberId)
        {
            CheckoutItem(memberId);
            Console.WriteLine($"Book '{Title}' checked out by member ID: {memberId}");
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"Type: Book, Pages: {NumberOfPages}");
        }
    }
}
