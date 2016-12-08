using System;
using MVC_Badge_System;

namespace FillPersonalDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice;
            bool exit = false;
            CSVReader reader = new CSVReader();

            while (!exit)
            {

                Console.WriteLine("Select Database to be filled:\n1) Users\n2) Badge\n3) Gift\n4) Exit");

                choice = Console.ReadLine();

                switch (choice[0])
                {
                    case '1':
                        reader.InputUserList();
                        break;
                    case '2':
                        reader.InputBadgeList();
                        break;
                    case '3':
                        reader.InputGiftList();
                        break;
                    case '4':
                        Console.WriteLine("C'est la vie!\n");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Hey!  None of that!\n");
                        break;
                }
            }
        }
    }
}
