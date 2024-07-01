using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public interface ILibraryItem
    {
        string Title { get; set; }
        string Author { get; set; }
        void Checkout(string memberId);
        void Return(string memberId);
        void Display();
    }

}
