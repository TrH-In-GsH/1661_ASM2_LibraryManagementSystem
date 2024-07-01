using System;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = Library.Instance;

            // Adding sample data
            try
            {
                library.AddItem(new Book { Title = "C# Programming", Author = "John Doe", Quantity = 5, NumberOfPages = 350 });
                library.AddItem(new DVD { Title = "Inception", Author = "Christopher Nolan", Quantity = 2, Duration = 2.5 });
                library.AddItem(new Magazine { Title = "Tech Monthly", Author = "Jane Smith", Quantity = 10, IssueNumber = 45 });

                library.AddMember("Alice Johnson");
                library.AddMember("Bob Williams");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding sample data: {ex.Message}");
            }

            // System menu  
            while (true)
            {
                Console.WriteLine("\nLibrary Management System");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Remove Item");
                Console.WriteLine("3. Add Member");
                Console.WriteLine("4. Remove Member");
                Console.WriteLine("5. Borrow Item");
                Console.WriteLine("6. Return Item");
                Console.WriteLine("7. Display Items");
                Console.WriteLine("8. Display Members");
                Console.WriteLine("9. Display Borrow History");
                Console.WriteLine("10. Extend Membership");
                Console.WriteLine("11. Search Items");
                Console.WriteLine("12. Search Members");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddItem(library);
                            break;
                        case 2:
                            RemoveItem(library);
                            break;
                        case 3:
                            AddMember(library);
                            break;
                        case 4:
                            RemoveMember(library);
                            break;
                        case 5:
                            BorrowItem(library);
                            break;
                        case 6:
                            ReturnItem(library);
                            break;
                        case 7:
                            library.DisplayItems();
                            break;
                        case 8:
                            library.DisplayMembers();
                            break;
                        case 9:
                            DisplayBorrowHistory(library);
                            break;
                        case 10:
                            ExtendMembership(library);
                            break;
                        case 11:
                            SearchItems(library);
                            break;
                        case 12:
                            SearchMembers(library);
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter a valid choice.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        static void AddItem(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Item Type (Book/DVD/Magazine) or 0 to cancel: ");
                    string itemType = Console.ReadLine();
                    if (itemType == "0") break;

                    Console.Write("Enter Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter Author: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter Quantity: ");
                    int quantity = int.Parse(Console.ReadLine());

                    if (itemType.Equals("Book", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("Enter Number of Pages: ");
                        int pages = int.Parse(Console.ReadLine());
                        library.AddItem(new Book { Title = title, Author = author, Quantity = quantity, NumberOfPages = pages });
                    }
                    else if (itemType.Equals("DVD", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("Enter Duration: ");
                        double duration = double.Parse(Console.ReadLine());
                        library.AddItem(new DVD { Title = title, Author = author, Quantity = quantity, Duration = duration });
                    }
                    else if (itemType.Equals("Magazine", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("Enter Issue Number: ");
                        int issue = int.Parse(Console.ReadLine());
                        library.AddItem(new Magazine { Title = title, Author = author, Quantity = quantity, IssueNumber = issue });
                    }
                    else
                    {
                        Console.WriteLine("Invalid Item Type.");
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while adding the item: {ex.Message}");
                }
            }
        }

        static void RemoveItem(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Item Title to Remove or 0 to cancel: ");
                    string title = Console.ReadLine();
                    if (title == "0") break;

                    Console.Write("Enter Item Type (Book/DVD/Magazine): ");
                    string itemType = Console.ReadLine();
                    library.RemoveItem(title, itemType);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while removing the item: {ex.Message}");
                }
            }
        }

        static void AddMember(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Member Name or 0 to cancel: ");
                    string name = Console.ReadLine();
                    if (name == "0") break;

                    library.AddMember(name);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while adding the member: {ex.Message}");
                }
            }
        }

        static void RemoveMember(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Member ID to Remove or 0 to cancel: ");
                    string memberId = Console.ReadLine();
                    if (memberId == "0") break;

                    library.RemoveMember(memberId);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while removing the member: {ex.Message}");
                }
            }
        }

        static void BorrowItem(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Member ID or 0 to cancel: ");
                    string memberId = Console.ReadLine();
                    if (memberId == "0") break;

                    Console.Write("Enter Item Title to Borrow: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter Item Type (Book/DVD/Magazine): ");
                    string itemType = Console.ReadLine();
                    library.BorrowItem(memberId, title, itemType);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while borrowing the item: {ex.Message}");
                }
            }
        }

        static void ReturnItem(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Member ID or 0 to cancel: ");
                    string memberId = Console.ReadLine();
                    if (memberId == "0") break;

                    Console.Write("Enter Item Title to Return: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter Item Type (Book/DVD/Magazine): ");
                    string itemType = Console.ReadLine();
                    library.ReturnItem(memberId, title, itemType);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while returning the item: {ex.Message}");
                }
            }
        }

        static void DisplayBorrowHistory(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Member ID to Display Borrow History or 0 to cancel: ");
                    string memberId = Console.ReadLine();
                    if (memberId == "0") break;

                    library.DisplayBorrowHistory(memberId);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while displaying the borrow history: {ex.Message}");
                }
            }
        }

        static void ExtendMembership(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Member ID to Extend Membership or 0 to cancel: ");
                    string memberId = Console.ReadLine();
                    if (memberId == "0") break;

                    library.ExtendMembership(memberId);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while extending the membership: {ex.Message}");
                }
            }
        }

        static void SearchItems(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Search Keyword or 0 to cancel: ");
                    string keyword = Console.ReadLine();
                    if (keyword == "0") break;

                    Console.Write("Enter Item Type (Optional): ");
                    string itemType = Console.ReadLine();
                    library.SearchItems(keyword, string.IsNullOrEmpty(itemType) ? null : itemType);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while searching items: {ex.Message}");
                }
            }
        }

        static void SearchMembers(Library library)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Search Keyword or 0 to cancel: ");
                    string keyword = Console.ReadLine();
                    if (keyword == "0") break;

                    library.SearchMembers(keyword);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid details.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input value is too large. Please enter a smaller number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while searching members: {ex.Message}");
                }
            }
        }
    }
}
