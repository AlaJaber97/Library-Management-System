using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS
{
    public class Manager: Administration
    {
        public Manager(string id, string password) : base(id, password) { }
        public void AddNewEmployee(string id, string password)
        {
            var Library = LibraryMS.Library.GetLibrary();
            if (!Library.Librarians.Any(Librarian => Librarian.Id == id))
            {
                Library.Librarians?.Add(new Librarian(id, password));
                Program.PrintErrorMessage("Done!");
            }
            Program.PrintErrorMessage("This is ID, it already exists. So can not add this employee with this ID");
        }
        public bool RemoveEmployee(string id)
        {
            var Library = LibraryMS.Library.GetLibrary();
            var Librarian = Library.Librarians?.SingleOrDefault(librarian => librarian.Id == id);
            if (Librarian != null)
            {
                Library.Librarians.Remove(Librarian);
                return true;
            }
            return false;
        }
    }
}
