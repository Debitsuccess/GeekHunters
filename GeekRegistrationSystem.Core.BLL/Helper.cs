using GeekRegistrationSystem.Core.BLL.Models;
using GeekRegistrationSystem.Core.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeekRegistrationSystem.Core.BLL
{
    public class Helper
    {
        //PrintSkills(skills)
        //Helper function to display all the avilable skills in the system to the DB.
        public static void PrintSkills(Entities<Skill> skills)
        {
            foreach (var skill in skills)
            {
                Console.WriteLine($"{skill.Id}.{skill.Name}");
            }
            Console.WriteLine("Enter skill(s): Type skill id. Use comma character ',' to select multiple skills then press Enter  )");
        }

        //IsValidString(stringValue)
        //Returns true if the string contains valid english charcaters only, otherwise, returns false.
        public static bool IsValidString(string stringValue)
        {
            string pattern = @"^\w+$";
            Regex regex = new Regex(pattern);

            // Compare a string against the regular expression
            return regex.IsMatch(stringValue);
        }

        //IsValidLength(stringValue)
        //Returns true if the string had a valid lenght as configured
        public static bool IsValidLength(string stringValue)
        {
            return stringValue.Length >= int.Parse(ConfigurationManager.AppSettings["MinimumStringLength"])
                && stringValue.Length <= int.Parse(ConfigurationManager.AppSettings["MaximumStringLength"]);
        }

        //ValidateString(stringValue)
        //Check if string is valid
        //Returns error messages if not a valid string.
        public static List<string> ValidateString(string stringValue)
        {
            List<string> errorMessages = new List<string>();
            if (!IsValidLength(stringValue))
                errorMessages.Add($"Invalid Length: Length must be between " +
                    $"{int.Parse(ConfigurationManager.AppSettings["MinimumStringLength"])} " +
                    $" And {int.Parse(ConfigurationManager.AppSettings["MaximumStringLength"])}.");
            if (!IsValidString(stringValue))
                errorMessages.Add($"  Invalid String: must be a valid english characters");
            return errorMessages;
        }

        //PrintList(values)
        //Print all strings to the consol
        public static void PrintList(List<string> messages)
        {
            foreach (string message in messages)
                Console.WriteLine($"ERROR: {message}");
        }
    }
}
