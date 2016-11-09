using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC_Badge_System;

namespace ConsoleApplication1
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
