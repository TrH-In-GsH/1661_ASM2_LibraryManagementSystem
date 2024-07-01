using System;

namespace LibraryManagementSystem
{
    public class DVD : LibraryItem
    {
        public double Duration { get; set; }

        public override void Checkout(string memberId)
        {
            CheckoutItem(memberId);
            Console.WriteLine($"DVD '{Title}' checked out by member ID: {memberId}");
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"Type: DVD, Duration: {Duration} hours");
        }
    }
}
