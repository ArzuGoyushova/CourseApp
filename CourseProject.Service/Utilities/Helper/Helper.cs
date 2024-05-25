using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Service.Utilities.Helper
{
    public static class Helper
    {
        public static void HelperMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void Border(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
        }
    }
}
