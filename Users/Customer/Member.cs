using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS
{
    public class Member : Client, IAuthentication
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Member(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }
        public void ShowBookReserve()
        {
            var Library = LibraryMS.Library.GetLibrary();
            var records = Library.Orders.Where(record =>
            record.MemberRef == this.UserName);

            foreach (var record in records)
                record.PrintInfo();
        }
        public override void RequestBuyBook(string id_book, int quantity)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Book = Library.Books.SingleOrDefault(book => book.SerialNumber == id_book);
            if (Book != null)
            {
                Library.Orders.Add(new Order
                {
                    MemberRef = this.UserName,
                    BookRef = Book.SerialNumber,
                    BookName = Book.Name,
                    Quntity = quantity,
                    TypeOrder = TypeOrder.Buy,
                    StatusOrder = StatusOrder.Check,
                });
                Program.PrintSuccessedMessage("this request is send to Librarian, please check with the librarian to complete the procedures..");
            }
            else
                Program.PrintErrorMessage("This book is not found!!");
        }
        public void RequestReserveBook(string id_book)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Book = Library.Books.SingleOrDefault(book => book.SerialNumber == id_book);
            if (Book != null)
            {
                Library.Orders.Add(new Order
                {
                    BookRef = Book.SerialNumber,
                    BookName = Book.Name,
                    MemberRef = this.UserName,
                    TypeOrder = TypeOrder.Reserver,
                    StatusOrder = StatusOrder.Check,
                });
                Program.PrintSuccessedMessage("this request is send to Librarian, please check with the librarian to complete the procedures..");
            }
            else
                Program.PrintErrorMessage("This book is not found!!");
        }
        public void RequestReturnBook(string id_order)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Order = Library.Orders.SingleOrDefault(order => order.Id == id_order);
            if (Order != null)
            {
                var Book = Library.Books.SingleOrDefault(book => book.SerialNumber == Order.BookRef);
                if (Book != null)
                {
                    Order.TypeOrder = TypeOrder.Return;
                    Order.StatusOrder = StatusOrder.Check;
                    Program.PrintSuccessedMessage("this request is send to Librarian, please check with the librarian to complete the procedures..");
                }
                else
                {
                    Order.StatusOrder = StatusOrder.Rejected;
                    Program.PrintErrorMessage("This book is not found!!");
                }
            }
            else
                Program.PrintErrorMessage("ID ORDER IS NOT CORRECT!!");
        }
        public void RequestExtendDate(string id_order, int total_day)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Order = Library.Orders.SingleOrDefault(order => order.Id == id_order);
            if (Order != null)
            {
                var Book = Library.Books.SingleOrDefault(book => book.SerialNumber == Order.BookRef);
                if (Book != null)
                {

                    Order.TypeOrder = TypeOrder.ExtendDate;
                    Order.Total_Day = total_day;
                    Order.StatusOrder = StatusOrder.Check;
                    Program.PrintSuccessedMessage("this request is send to Librarian, please check with the librarian to complete the procedures..");
                }
                else
                    Program.PrintErrorMessage("This book is not found!!");
            }
            else
                Program.PrintErrorMessage("ID ORDER IS NOT CORRECT!!");
        }

        public bool IsAuthentication(string username, string password)
        {
            return string.Equals(UserName, username) && string.Equals(Password, password);
        }
    }
}
