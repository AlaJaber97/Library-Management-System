using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS
{
    public class NotMember : Client
    {
        public override void RequestBuyBook(string id_book, int quantity)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Book = Library.Books?.SingleOrDefault(book => book.SerialNumber == id_book);
            if (Book != null)
            {
                Library.Orders.Add(new Order
                {
                    BookRef = Book.SerialNumber,
                    BookName = Book.Name,
                    MemberRef = "Guest",
                    Quntity = quantity,
                    TypeOrder = TypeOrder.Buy,
                    StatusOrder = StatusOrder.Check,
                });
                Program.PrintSuccessedMessage("this request is send to Librarian, please check with the librarian to complete the procedures..");
            }
            else
                Program.PrintErrorMessage("This book is not found!!");
        }
    }
}
