using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS
{
    public class Librarian : Administration
    {
        public Librarian(string id, string password) : base(id, password) { }
        public void ShowOrders()
        {
            var Library = LibraryMS.Library.GetLibrary();
            var records = Library.Orders;

            foreach (var record in records)
                record.PrintInfo();
        }
        public void ManageRequest()
        {
            var Library = LibraryMS.Library.GetLibrary();
            foreach (var request in Library.Orders.Where(order=> order.StatusOrder == StatusOrder.Check))
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                   Console.Write($"| Type Request: "); Program.PrintWarningMessage($"{request.TypeOrder}");
               Console.WriteLine($"| Book Name: {request.BookName}\n" +
                                 $"| Book Serial: {request.BookRef}\n" +
                                 $"{(request.TypeOrder == TypeOrder.Buy ? $"| Quntity: {request.Quntity}\n" : "\r") }" +
                                 $"{(request.TypeOrder == TypeOrder.ExtendDate ? $"| Total Day: {request.Total_Day}\n" : "\r") }" +
                                 $"==========================================\n");
                Console.Write("Enter 1 for (Accept) and 2 for (Reject): "); var choise = Console.ReadLine().Trim();
                if (choise == "1")
                {
                    switch (request.TypeOrder)
                    {
                        case TypeOrder.Reserver:
                            if (!ConfirmReserveBook(request))
                                Program.PrintErrorMessage("There is not enough quantity to purchase this book, So system Reject this Order");
                            else
                                Program.PrintSuccessedMessage($"OK, a copy of the book ({request.BookName}) has been reserved, for this member called ({request.MemberRef})");
                            break;
                        case TypeOrder.Return:
                            if (ConfirmReturnBook(request))
                                Program.PrintSuccessedMessage($"Ok, a copy of this book has been retrieved");
                            else
                                Program.PrintErrorMessage("Return book process was failed!!, So system Reject this Order");
                            break;
                        case TypeOrder.ExtendDate:
                            if(ConfirmExtendDate(request))
                                Program.PrintSuccessedMessage($"Ok, the retrieval period for a copy of the Book of ({request.BookName}) has been extended ({request.Total_Day}) days, for this member called ({request.MemberRef})");
                            else
                                Program.PrintErrorMessage("Extend date book process was failed!!, So system Reject this Order");
                            break;
                        case TypeOrder.Buy:
                            if (ConfirmBuyBook(request))
                                this.CreateBill(request);
                            else
                                Program.PrintErrorMessage("There is not enough quantity to purchase this book, So system Reject this Order");
                            break;
                    }
                }
                else if (choise == "2")
                {
                    request.StatusOrder = StatusOrder.Rejected;
                }
                Console.WriteLine("Press enter to continue, press 0 to exit");
                if (Console.ReadKey().KeyChar == '0')
                    return;
            }
            Console.Clear();
            Console.WriteLine("There is no more request, Thank you for good work!"); 
            Console.WriteLine("Press enter to continue");Console.ReadKey();
        }
        public bool AddNewBook(string serialnumber, string name,List<string> keywords, int copies_number, double price)
        {
            var Library = LibraryMS.Library.GetLibrary();
            if (Library.Books.Where(book => book.SerialNumber == serialnumber).Count() == 0)
            {
                var Book = new Book(serialnumber, name, keywords);
                Book.SetInfoBook(copies_number, price);
                Library.Books.Add(Book);
                return true;
            }
            return false;
        }
        public bool RemoveBook(string serialnumber)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Book = Library.Books.SingleOrDefault(item => item.SerialNumber == serialnumber);
            if (Book != null)
            {
                Library.Books.Remove(Book);
                return true;
            }
            return false;
        }
        private bool ConfirmBuyBook(Order order)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Book = Library.Books.SingleOrDefault(item => item.SerialNumber == order.BookRef);
            if (Book != null)
            {
                var isDone = Book.SellingCopies(order.Quntity); 
                order.LibrarianRef = this.Id;
                if (isDone)
                {
                    order.StatusBook = StatusBook.Sold;
                    order.StatusOrder = StatusOrder.Accepted;
                    return true;
                }
                order.StatusOrder = StatusOrder.Rejected;
                return false;
            }
            return false;
        }
        private bool ConfirmReserveBook(Order order)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Book = Library.Books.SingleOrDefault(item => item.SerialNumber == order.BookRef);
            if (Book != null)
            {
                var Copy = Book.ReservingCopy();
                order.LibrarianRef = this.Id;
                if (Copy != null)
                {
                    order.LibrarianRef = this.Id;
                    order.BookName = Book.Name;
                    order.CopyRef = Copy.Id;
                    order.Quntity = 1;
                    order.DueDate = Copy.DueDateReserve.Value;
                    order.StatusBook = StatusBook.Reserve;
                    order.StatusOrder = StatusOrder.Accepted;
                    return true;
                }
                order.StatusOrder = StatusOrder.Rejected;
                return false;
            }
            return false;
        }
        private bool ConfirmReturnBook(Order order)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Book = Library.Books.SingleOrDefault(item => item.SerialNumber == order.BookRef);
            if (Book != null)
            {
                var isDone = Book.ReturnCopy(order.CopyRef);
                order.LibrarianRef = this.Id;
                if (isDone)
                {
                    order.StatusBook = StatusBook.Available;
                    order.StatusOrder = StatusOrder.Accepted;
                    return true;
                }
                order.StatusOrder = StatusOrder.Rejected;
                return false;
            }
            return false;
        }
        private bool ConfirmExtendDate(Order order)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Book = Library.Books.SingleOrDefault(item => item.SerialNumber == order.BookRef);
            if (Book != null)
            {
                var isDone = Book.ExtendDueCopy(order.CopyRef, order.Total_Day);
                order.LibrarianRef = this.Id;
                if (isDone)
                {
                    order.DueDate = order.DueDate.Value.AddDays(order.Total_Day);
                    order.StatusBook = StatusBook.Reserve;
                    order.StatusOrder = StatusOrder.Accepted;
                    return true;
                }
                order.StatusOrder = StatusOrder.Rejected;
                return false;
            }
            return false;
        }
        private void CreateBill(Order Record)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("====================================================\n" +
                              "|                      Bill                        |\n" +
                              "====================================================\n" +
                              "|                                                   \n" +
                             $"|  Employee ID: {Record.LibrarianRef}               \n" +
                             $"|  Member User Name: {Record.MemberRef}             \n" +
                             $"|  Serial Number Book: {Record.BookRef}             \n" +
                             $"|  Name Book: {Record.BookName}                     \n" +
                             $"|  Quntity: {Record.Quntity}                        \n" +
                              "|                                                   \n" +
                              "====================================================\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
