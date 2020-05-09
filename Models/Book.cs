using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS
{
    public class Book
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<Copy> Copies { get; set; }
        public List<string> KeyWords { get; set; }
        public int AvailableCopies => Copies != null ? Copies.Count(copy => copy.Status == StatusBook.Available) : 0;
        public int NumberReserve => Copies != null ? Copies.Count(copy => copy.Status == StatusBook.Reserve) : 0;

        public Book(string serialnumber,string name, List<string> keywords)
        {
            this.SerialNumber = serialnumber;
            this.Name = name;
            this.KeyWords = keywords;
            this.Copies = new List<Copy>();
        }
        public void SetInfoBook(int available_copies, double price)
        {
            this.Price = price;
            int IdCopy = 1;
            while (IdCopy++ <= available_copies)
                Copies.Add(new Copy(IdCopy.ToString()));
        }
        public bool SellingCopies(int quntity)
        {
            var AvailableCopies = Copies.Where(copy => copy.Status == StatusBook.Available).ToList();
            if (AvailableCopies.Count() >= quntity)
            {
                AvailableCopies.Take(quntity).ToList()
                        .ForEach(copy => copy.Status = StatusBook.Sold);
                return true;
            }
            return false;
        }
        public Copy ReservingCopy()
        {
            var AvailableCopies = Copies.Where(copy => copy.Status == StatusBook.Available);
            if (AvailableCopies.Count() >= 1)
            {
                var Copy = AvailableCopies.First();
                Copy.DueDateReserve = DateTime.Now.AddDays(7);
                Copy.Status = StatusBook.Reserve;
                return Copy;
            }
            return null;
        }
        public bool ReturnCopy(string id_copy)
        {
            var Copy = Copies.SingleOrDefault(copy => copy.Id == id_copy);
            if (Copy != null)
            {
                Copy.Status = StatusBook.Available;
                Copy.DueDateReserve = null;
                return true;
            }
            return false;
        }
        public bool ExtendDueCopy(string id_copy, int number_day)
        {
            var Copy = Copies.SingleOrDefault(copy => copy.Id == id_copy);
            if (Copy != null)
            {
                Copy.DueDateReserve = Copy.DueDateReserve.Value.AddDays(number_day);
                return true;
            }
            return false;
        }
        public void PrintInfo()
        {
            Console.WriteLine("======================================\n" +
                              $"| Serial Number: {this.SerialNumber} \n" +
                              $"| Name: {this.Name}                  \n" +
                              $"| Price: {this.Price} JD             \n" +
                              $"=====================================\n");
        }
    }
}
