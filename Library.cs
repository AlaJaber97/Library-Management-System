using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS
{
    public sealed class Library
    {
        private static readonly Library instance = new Library();

        public List<Book> Books;
        public List<Order> Orders;

        public Manager Manager;
        public List<Librarian> Librarians;
        public List<Member> Members;
        public NotMember Guest;
        private Library()
        {
            Manager = new Manager("admin", "admin");
            //Can not user Manager.AddNewEmployee("lib","lib"), because that will cause recursion

            Librarians = new List<Librarian>()
            {
                new Librarian("lib","lib")
            };

            //I made it static to simplify and simplify the code, 
            //but in reality it should be the librarian's job
            Members = new List<Member>()
            {
                new Member("mem","mem")
            };

            Guest = new NotMember();

            //Can not user Librarian.AddNewBook(.........), because that will cause recursion
            Books = new List<Book>()
            {
                new Book("1","Knight Of Joy", new List<string>{ "knight","of","joy" }),
                new Book("2","Wife Of Nightmares", new List<string>{ "wife", "of", "nightmares" }),
                new Book("3","Slaves Of The Gods", new List<string>{ "slaves", "of", "the", "god", "gods" }),
                new Book("4","Rebels Of Water", new List<string>{ "rebels", "of", "water" }),
            };
            Books.ForEach(book => book.SetInfoBook(10, 10.60));

            Orders = new List<Order>();
        }

        public static Library GetLibrary()
        {
            return instance;
        }
    }
}
