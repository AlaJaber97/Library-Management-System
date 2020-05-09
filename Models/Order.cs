using System;

namespace LibraryMS
{
    public class Order
    {
        public string Id { get; set; }
        public string LibrarianRef { get; set; }
        public string MemberRef { get; set; }
        public string BookRef { get; set; }
        public string BookName { get; set; }
        public string CopyRef { get; set; }
        public int Quntity { get; set; } = 1;
        public int Total_Day { get; set; } = 7;
        public DateTime? DueDate { get; set; }
        public StatusBook StatusBook { get; set; }
        public TypeOrder TypeOrder { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public Order()
        {
            this.Id = (new Random()).Next(100, 500).ToString();
        }
        public void PrintInfo()
        {
            Console.WriteLine("==========================================\n" +
                             $"| Order ID: {this.Id}                     \n" +
                             $"| Librarian ID: {this.LibrarianRef}       \n" +
                             $"| Member User Name: {this.MemberRef}      \n" +
                             $"| Book Serial Number: {this.BookRef}      \n" +
                             $"| Book Name: {this.BookName}              \n" +
                             $"{(this.TypeOrder != TypeOrder.Buy ? $"| Copy Id: {this.CopyRef}\n" : "\r") }" +
                             $"| Quntity: {this.Quntity}                 \n" +
                             $"{(this.TypeOrder != TypeOrder.Buy ? $"| Due Date: {this.DueDate}\n" : "\r") }" +
                             $"| Status Book: {this.StatusBook}          \n" +
                             $"| Type Order: {this.TypeOrder}              ");

               Console.Write($"| Status Order: ");
            if (this.StatusOrder == StatusOrder.Check)
                Program.PrintWarningMessage($"{this.StatusOrder}");
            else if (this.StatusOrder == StatusOrder.Accepted)
                Program.PrintSuccessedMessage($"{this.StatusOrder}");
            else if (this.StatusOrder == StatusOrder.Rejected)
                Program.PrintErrorMessage($"{this.StatusOrder}");
            Console.WriteLine("==========================================\n");
        }
    }
}