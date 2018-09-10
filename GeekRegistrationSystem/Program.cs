using GeekRegistrationSystem.Common;
using GeekRegistrationSystem.Core.BLL;
using GeekRegistrationSystem.Core.BLL.Models;
using GeekRegistrationSystem.Core.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GeekRegistrationSystem
{
    class Program
    {
        //Main entry point to the consol application
        //Geek Registration System
        static void Main(string[] args)
        {
            #region Initialize DataAccessAdapter
            DatabaseService.Initialize(ConfigurationManager.ConnectionStrings["GeekRegistrationConnectionString"].ConnectionString);
            #endregion
            int userChoice;
            do
            {
                userChoice = Helper.DisplayMenu();     
                if (userChoice == (int)Enums.MainMenu.AddCandidate)
                {
                    Candidate candidate = new Candidate();
                    candidate.AddCandidate();
                }
                else if (userChoice == (int)Enums.MainMenu.ListAllCandidates)
                {
                    Candidate candidate = new Candidate();
                    candidate.ViewAllCandidates();
                }
                else if (userChoice == (int)Enums.MainMenu.FilterCandidatesByASkill)
                {
                    Candidate candidate = new Candidate();
                    candidate.ViewCandidatesBySkill();
                }
                else if (userChoice == (int)Enums.MainMenu.Exit)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice --> Please type a number from 1 to 4 from the Main Menu");
                }
            }
            while (userChoice != (int)Enums.MainMenu.Exit);
        }
    }
}
