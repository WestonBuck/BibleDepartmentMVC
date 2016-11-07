using MVC_Badge_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSVReader
{
    class Program
    {
        static void Main(string[] args)
        {
            CSVReader reader = new CSVReader();

            reader.InputUserList();
        }
    }
}
