using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public class Library
    {
        private static Library instance;
        private static readonly object lockObject = new object();

        private Dictionary<string, LibraryItem> items = new Dictionary<string, LibraryItem>();
        private Dictionary<string, Member> members = new Dictionary<string, Member>();

        private Library() { }

        public static Library Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new Library();
                    }
                    return instance;
                }
            }
        }

        public void AddItem(LibraryItem item)
        {
            if (items.ContainsKey(item.Title))
            {
                throw new ArgumentException("Item with this title already exists.");
            }
            items[item.Title] = item;
            Console.WriteLine($"Item '{item.Title}' added.");
        }

        public void RemoveItem(string title, string type)
        {
            if (!items.ContainsKey(title))
            {
                throw new KeyNotFoundException("Item not found.");
            }
            if (items[title].GetType().Name != type)
            {
                throw new InvalidOperationException("Item type mismatch.");
            }
            items.Remove(title);
            Console.WriteLine($"Item '{title}' removed.");
        }

        public void AddMember(string name)
        {
            string memberId = GenerateMemberId();
            if (members.ContainsKey(memberId))
            {
                throw new ArgumentException("Member ID already exists.");
            }
            members[memberId] = new Member { MemberId = memberId, Name = name, CreatedDate = DateTime.Now, ExpiryDate = DateTime.Now.AddMonths(1) };
            Console.WriteLine($"Member '{name}' added with ID: {memberId}");
        }

        public void RemoveMember(string memberId)
        {
            if (!members.ContainsKey(memberId))
            {
                throw new KeyNotFoundException("Member not found.");
            }
            members.Remove(memberId);
            Console.WriteLine($"Member with ID: {memberId} removed.");
        }

        public void BorrowItem(string memberId, string title, string type)
        {
            if (!members.ContainsKey(memberId))
            {
                throw new KeyNotFoundException("Member not found.");
            }
            if (!items.ContainsKey(title) || items[title].GetType().Name != type)
            {
                throw new KeyNotFoundException("Item not found or type mismatch.");
            }
            if (members[memberId].HasOverdueItems())
            {
                throw new InvalidOperationException("Member has overdue items.");
            }
            if (members[memberId].IsExpired())
            {
                throw new InvalidOperationException("Member's membership has expired.");
            }
            members[memberId].BorrowItem(items[title]);
        }

        public void ReturnItem(string memberId, string title, string type)
        {
            if (!members.ContainsKey(memberId))
            {
                throw new KeyNotFoundException("Member not found.");
            }
            if (!items.ContainsKey(title) || items[title].GetType().Name != type)
            {
                throw new KeyNotFoundException("Item not found or type mismatch.");
            }
            members[memberId].ReturnItem(items[title]);
        }

        public void DisplayItems()
        {
            foreach (var item in items.Values)
            {
                item.Display();
            }
        }

        public void DisplayMembers()
        {
            foreach (var member in members.Values)
            {
                member.Display();
            }
        }

        public void DisplayBorrowHistory(string memberId)
        {
            if (!members.ContainsKey(memberId))
            {
                throw new KeyNotFoundException("Member not found.");
            }
            members[memberId].DisplayBorrowHistory();
        }

        public void ExtendMembership(string memberId)
        {
            if (!members.ContainsKey(memberId))
            {
                throw new KeyNotFoundException("Member not found.");
            }
            members[memberId].ExtendMembership();
        }

        public void SearchItems(string keyword, string type = null)
        {
            var results = items.Values.Where(item =>
                item.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                item.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(type))
            {
                results = results.Where(item => item.GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase));
            }

            foreach (var item in results)
            {
                item.Display();
            }
        }

        public void SearchMembers(string keyword)
        {
            var results = members.Values.Where(member =>
                member.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                member.MemberId.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            foreach (var member in results)
            {
                member.Display();
            }
        }

        private string GenerateMemberId()
        {
            // Initialize the prefix and counter
            char prefix = 'A';
            int counter = members.Count % 1000;

            // Generate the initial ID
            string id = $"{prefix}{counter:D3}";

            // Loop to find a unique ID
            while (members.ContainsKey(id))
            {
                // Increment the counter
                counter++;
                // Roll over if the counter exceeds 999
                if (counter > 999)
                {
                    counter = 0;
                    prefix++;
                }
                // Generate the new ID with updated prefix and counter
                id = $"{prefix}{counter:D3}";
            }

            return id;
        }

    }
}
