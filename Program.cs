using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS
{
    class Program
    {
        public static Library Library = Library.GetLibrary();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========================================================\n" +
                                  "|                                                       |\n" +
                                  "|                        Main Menu                      |\n" +
                                  "=========================================================\n" +
                                  "|                                                        \n" +
                                  "| 1. Login                                               \n" +
                                  "| 2. Buy Book                                            \n" +
                                  "| 3. Search                                              \n" +
                                  "| 4. Exit                                                \n" +
                                  "|                                                        \n" +
                                  "=========================================================\n");
            TryAgain:
                Console.Write("Enter your choise: ");
                if (int.TryParse(Console.ReadLine().Trim(), out int OptionChooes))
                {
                    Console.Clear();
                    switch (OptionChooes)
                    {
                        case 1:
                            LoginToSystem();
                            break;
                        case 2:
                            {
                                EnterSerial:
                                Console.Write("Enter serial number book: ");
                                var serial = Console.ReadLine().Trim(); 
                                if (string.IsNullOrWhiteSpace(serial))
                                    goto EnterSerial;
                                EnterQuntity: 
                                Console.Write("Enter quntity: "); 
                                if(!int.TryParse(Console.ReadLine().Trim(), out int quntity))
                                    goto EnterQuntity;

                                Library.Guest.RequestBuyBook(serial, quntity);
                                Console.WriteLine("Press enter to continue"); Console.ReadKey();
                            }
                            break;
                        case 3:
                            {
                                Console.Write("Enter keyword: "); var keyword = Console.ReadLine().Trim();
                                var Books = Library.Guest.SearchBook(keyword);
                                if (Books.Count() > 0)
                                {
                                    foreach (var book in Books)
                                        book.PrintInfo();
                                }
                                else
                                {
                                    PrintErrorMessage($"you search -{keyword}- did not match any books");
                                }
                                Console.WriteLine("Press enter to continue"); Console.ReadKey();
                            }
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            PrintErrorMessage("Invalid choise! please try again enter your choise");
                            goto TryAgain;
                    }
                }
                else
                {
                    PrintErrorMessage("Invalid choise! please try again enter your choise");
                    goto TryAgain;
                }
            }
        }

        private static void LoginToSystem()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========================================================\n" +
                                  "|                                                       |\n" +
                                  "|                        Main Menu                      |\n" +
                                  "=========================================================\n" +
                                  "|                                                        \n" +
                                  "| 1. Login as Manager                                    \n" +
                                  "| 2. Login as Librarian                                  \n" +
                                  "| 3. Login as Member                                     \n" +
                                  "| 4. Back                                                \n" +
                                  "|                                                        \n" +
                                  "=========================================================\n");
            TryAgain:
                Console.Write("Enter your choise: ");
                if (int.TryParse(Console.ReadLine().Trim(), out int OptionChooes))
                {
                    switch (OptionChooes)
                    {
                        case 1:
                            Console.Clear();
                            ModeManager();
                            break;
                        case 2:
                            Console.Clear();
                            ModeLibrarian();
                            break;
                        case 3:
                            Console.Clear();
                            ModeMember();
                            break;
                        case 4:
                            Console.Clear();
                            return;
                        default:
                            PrintErrorMessage("Invalid choise! please try again enter your choise");
                            goto TryAgain;
                    }
                }
                else
                {
                    PrintErrorMessage("Invalid choise! please try again enter your choise");
                    goto TryAgain;
                }
            }
        }
        private static void ModeMember()
        {
            Console.Write("Enter User Name: "); var username = Console.ReadLine().Trim();
            Console.Write("Enter Password: "); var password = Console.ReadLine().Trim();
            var Member = Library.Members.SingleOrDefault(member => member.IsAuthentication(username, password));
            if (Member != null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=========================================================\n" +
                                      "|                                                       |\n" +
                                      "|                        Main Menu                      |\n" +
                                      "=========================================================\n" +
                                      "|                                                        \n" +
                                      "| 1. Request Buy book                                    \n" +
                                      "| 2. Request reserve book                                \n" +
                                      "| 3. Request return book                                 \n" +
                                      "| 4. Request extend date book                            \n" +
                                      "| 5. Search                                              \n" +
                                      "| 6. Show Orders                                         \n" +
                                      "| 7. Logout                                              \n" +
                                      "|                                                        \n" +
                                      "=========================================================\n");
                TryAgain:
                    Console.Write("Enter your choise: ");
                    if (int.TryParse(Console.ReadLine().Trim(), out int OptionChooes))
                    {
                        Console.Clear();
                        switch (OptionChooes)
                        {
                            case 1:
                                {
                                    Console.Write("Enter serial number book: "); var serial = Console.ReadLine().Trim();
                                    Console.Write("Enter quntity: "); var quntity = int.Parse(Console.ReadLine().Trim());
                                    Member.RequestBuyBook(serial, quntity);
                                    Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                }
                                break;
                            case 2:
                                {
                                    Console.Write("Enter serial number book: "); var serial = Console.ReadLine().Trim();
                                    Member.RequestReserveBook(serial);
                                    Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                }
                                break;
                            case 3:
                                {
                                    Console.Write("Enter Id Order: "); var id_order = Console.ReadLine().Trim();
                                    Member.RequestReturnBook(id_order);
                                    Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                }
                                break;
                            case 4:
                                {
                                    Console.Write("Enter Id Order: "); var id_order = Console.ReadLine().Trim();
                                    Console.Write("Enter number day to extend: "); var total_day = int.Parse(Console.ReadLine().Trim());
                                    Member.RequestExtendDate(id_order, total_day);
                                    Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                }
                                break;
                            case 5:
                                {
                                    Console.Write("Enter keyword: "); var keyword = Console.ReadLine().Trim();
                                    var Books = Library.Guest.SearchBook(keyword);
                                    if (Books.Count() > 0)
                                    {
                                        foreach (var book in Books)
                                            book.PrintInfo();
                                    }
                                    else
                                    {
                                        PrintErrorMessage($"you search -{keyword}- did not match any books");
                                    }
                                    Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                }
                                break;
                            case 6:
                                Member.ShowBookReserve();
                                Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                break;
                            case 7:
                                Console.Clear();
                                return;
                            default:
                                PrintErrorMessage("Invalid choise! please try again enter your choise");
                                goto TryAgain;
                        }
                    }
                    else
                    {
                        PrintErrorMessage("Invalid choise! please try again enter your choise");
                        goto TryAgain;
                    }
                }
            }
            else
            {
                PrintErrorMessage("username and password do not match");
                Console.WriteLine("Press enter to continue"); Console.ReadKey();
            }
        }
        private static void ModeLibrarian()
        {
            Console.Write("Enter ID: "); var id = Console.ReadLine().Trim();
            Console.Write("Enter Password: "); var password = Console.ReadLine().Trim();
            var Librarian = Library.Librarians.SingleOrDefault(Lib => Lib.IsAuthentication(id, password));
            if (Librarian != null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=========================================================\n" +
                                      "|                                                       |\n" +
                                      "|                        Main Menu                      |\n" +
                                      "=========================================================\n" +
                                      "|                                                        \n" +
                                      "| 1. Add new book                                        \n" +
                                      "| 2. Remove book                                         \n" +
                                      "| 3. Review Request Book                                 \n" +
                                      "| 4. Show All Orders                                     \n" +
                                      "| 5. Logout                                              \n" +
                                      "|                                                        \n" +
                                      "=========================================================\n");
                TryAgain:
                    Console.Write("Enter your choise: ");
                    if (int.TryParse(Console.ReadLine().Trim(), out int OptionChooes))
                    {
                        Console.Clear();
                        switch (OptionChooes)
                        {
                            case 1:
                                {
                                    Console.WriteLine("=========================================================\n" +
                                                      "|                                                       |\n" +
                                                      "|             Please Input Information Book             |\n" +
                                                      "=========================================================\n");
                                        Console.Write("| Enter serial number book: "); var serialbook = Console.ReadLine().Trim();
                                        Console.Write("| Enter name book: "); var namebook = Console.ReadLine().Trim();
                                        Console.Write("| Enter number of copies: "); var number_of_copise = int.Parse(Console.ReadLine().Trim());
                                        Console.Write("| Enter price book: "); var price = double.Parse(Console.ReadLine().Trim());
                                        Console.Write("| Enter 3 keywords: "); List<string> keywords = new List<string>();
                                    for (int i = 0; i < 3; i++) keywords.Add(Console.ReadLine().ToLower().Trim());
                                    Console.WriteLine("=========================================================\n");

                                    var isDone = Librarian.AddNewBook(serialbook, namebook, keywords, number_of_copise, price);
                                    if (isDone)
                                        PrintSuccessedMessage("this book is added..");
                                    else
                                        PrintSuccessedMessage("this book is exist from previous, so can not add..");
                                    Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                }
                                break;
                            case 2:
                                {
                                    Console.Write("Enter serial number book: "); var serial = Console.ReadLine().Trim();
                                    if (Librarian.RemoveBook(serial))
                                        PrintSuccessedMessage("this book is removed..");
                                    Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                }
                                break;
                            case 3:
                                Librarian.ManageRequest(); 
                                break;
                            case 4:
                                Librarian.ShowOrders();
                                Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                return;
                            case 5:
                                Console.Clear();
                                return;
                            default:
                                PrintErrorMessage("Invalid choise! please try again enter your choise");
                                goto TryAgain;
                        }
                    }
                    else
                    {
                        PrintErrorMessage("Invalid choise! please try again enter your choise");
                        goto TryAgain;
                    }
                }
            }
            else
            {
                PrintErrorMessage("username and password do not match");
                Console.WriteLine("Press enter to continue"); Console.ReadKey();
            }
        }
        private static void ModeManager()
        {
            Console.Write("Enter ID: "); var id = Console.ReadLine().Trim();
            Console.Write("Enter Password: "); var password = Console.ReadLine().Trim();
            if (Library.Manager.IsAuthentication(id, password))
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=========================================================\n" +
                                      "|                                                       |\n" +
                                      "|                        Main Menu                      |\n" +
                                      "=========================================================\n" +
                                      "|                                                        \n" +
                                      "| 1. Add new librarian                                   \n" +
                                      "| 2. Remove librarian                                    \n" +
                                      "| 3. Logout                                              \n" +
                                      "|                                                        \n" +
                                      "=========================================================\n");
                TryAgain:
                    Console.Write("Enter your choise: ");
                    if (int.TryParse(Console.ReadLine().Trim(), out int OptionChooes))
                    {
                        Console.Clear();
                        switch (OptionChooes)
                        {
                            case 1:
                                Console.Write("Enter ID Librarian: "); var idlibrarian = Console.ReadLine().Trim();
                                Console.Write("Enter Password Default: "); var passwordlibrarian = Console.ReadLine().Trim();
                                Library.Manager.AddNewEmployee(idlibrarian, passwordlibrarian);
                                PrintSuccessedMessage("this Librarian is added..");
                                Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                break;
                            case 2:
                                Console.Write("Enter ID Librarian: ");
                                if (!Library.Manager.RemoveEmployee(Console.ReadLine().Trim()))
                                    PrintErrorMessage("this Librarian is not exist in your library!!");
                                else
                                    PrintSuccessedMessage("this Librarian is removed..");
                                Console.WriteLine("Press enter to continue"); Console.ReadKey();
                                break;
                            case 3:
                                Console.Clear();
                                return;
                            default:
                                PrintErrorMessage("Invalid choise! please try again enter your choise");
                                goto TryAgain;
                        }
                    }
                    else
                    {
                        PrintErrorMessage("Invalid choise! please try again enter your choise");
                        goto TryAgain;
                    }
                }
            }
            else
            {
                PrintErrorMessage("username and password do not match");
                Console.WriteLine("Press enter to continue"); Console.ReadKey();
            }
        }

        public static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void PrintWarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void PrintSuccessedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

}
