using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekRegistrationSystem
{
    public class Helper
    {
        //DisplayMenu
        //Displays main menu options to the user
        static public int DisplayMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Geek Registration System");
            Console.WriteLine("");
            Console.WriteLine("1. Add a candidate");
            Console.WriteLine("2. List all candidates");
            Console.WriteLine("3. Filter candidates by a skill");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Type your choice then press Enter:");
            var userChoice = Console.ReadLine();
            if (IsNumeric(userChoice))
                return int.Parse(userChoice);
            else
                return -1;
        }
        //IsNumberic(value)
        //Returns true if the string is a numeric value, otherwise, returns false.
        static public bool IsNumeric(string value)
        {
            int output = -1;
            return int.TryParse(value, out output);
        }
    }
}
