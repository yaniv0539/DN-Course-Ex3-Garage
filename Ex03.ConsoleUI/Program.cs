using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            ConsoleUI garageUI = new ConsoleUI();
            garageUI.RunGarageSystem();

            System.Console.WriteLine("Press 'Enter' to continue");
            System.Console.ReadLine();
        }
    }
}
