using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS
{
    public abstract class Client
    {
        public abstract void RequestBuyBook(string id_book, int quantity);
        public List<Book> SearchBook(string keyword)
        {
            var Library = LibraryMS.Library.GetLibrary();
            return Library.Books?.Where(book => book.KeyWords.Contains(keyword.ToLower()))?.ToList();
        }
    }
}
