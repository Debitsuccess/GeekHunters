using System;
using GeekRegistrationSystem.Core.DAL.Interfaces;
using System.ComponentModel;
using GeekRegistrationSystem.Core.BLL.Interfaces;
using GeekRegistrationSystem.Core.DAL;
using GeekRegistrationSystem.Core.BLL.Models;
using System.Collections.Generic;
using System.Linq;

namespace GeekRegistrationSystem.Core.BLL
{
    public class Candidate : IEntity<Candidate>, ICandidate
    {
        #region Table Fields

        [DisplayName("Identity")]
        [Category("Column")]
        [DataObjectFieldAttribute(true, true, false)]//Primary key attribute 
        public long Id { get; set; }

        [DisplayName("First Name")]
        [Category("Column")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Category("Column")]
        public string LastName { get; set; }


        #endregion

        #region Initialize
        public Candidate() { }
        #endregion

        #region Implementations
        //ViewAllCandidates()
        //Display all candidates with associated skills.
        public void ViewAllCandidates()
        {


            Entities<Candidate> candidates = Context.Instance.Candidate.Collection;
            Entities<Skill> skills = Context.Instance.Skill.Collection;
            Entities<CandidateSkill> candidatesSkills = Context.Instance.CandidateSkill.Collection;
            int count = 1;
            foreach (var candidate in candidates)
            {

                List<string> candidateSkillsNames = new List<string>();
                List<CandidateSkill> candidateSkills = candidatesSkills.Where(c => c.CandidateId == candidate.Id).ToList();
                foreach (var item in candidateSkills)
                {
                    candidateSkillsNames.AddRange(skills.Where(s => s.Id == item.SkillId).Select(s2 => s2.Name));
                }
                string selectedSkills = candidateSkillsNames.Any() ? string.Join(",", candidateSkillsNames) : "has no selected skills";
                Console.WriteLine($"{count}.{candidate.FirstName} {candidate.LastName}, Skills: {selectedSkills}");
                count++;
            }
        }

        //AddCandidate
        //Recieve candidate details from the user and add call to insert a record in the db
        public void AddCandidate()
        {
            Entities<Candidate> candidates = Context.Instance.Candidate.Collection;
            Entities<Skill> skills = Context.Instance.Skill.Collection;

            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();
            List<string> errorMessages = Helper.ValidateString(firstName);
            if (errorMessages != null && errorMessages.Any())
            {
                Console.WriteLine($"First name is invalid");
                Helper.PrintList(errorMessages);
                Console.WriteLine("Failed to add a candidate. Please try again");
                return;
            }
            

            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine();
            errorMessages.Clear();
            if (errorMessages != null && errorMessages.Any())
            {
                Console.WriteLine($"Last name is invalid");
                Helper.PrintList(errorMessages);
                Console.WriteLine("Failed to add a candidate. Please try again");
                return;
            }
            Helper.PrintSkills(skills);

            Console.WriteLine("Type 0 then press Enter to return to the Main Menu");
        TryAgain: string readLine = Console.ReadLine();
            if (readLine == "0")
                return;
            List<string> filterSkillIds = readLine.Split(',').ToList();
            long tempId;
            foreach (var item in filterSkillIds)
            {
                if (!long.TryParse(item, out tempId) ||
                    !skills.Select(s => s.Id).Contains(long.Parse(item)))
                {
                    Console.WriteLine($"{item} is an invalid skill");
                    Console.WriteLine($"Re-Enter a valid skill(s) from the list");
                    Helper.PrintSkills(skills);
                    goto TryAgain;
                }
            }

            Candidate candidate = new Candidate();
            candidate.FirstName = firstName;
            candidate.LastName = lastName;
            Add(candidate);

            foreach (var item in filterSkillIds)
            {
                CandidateSkill candidateSkill = new CandidateSkill();
                candidateSkill.CandidateId = Context.Instance.Candidate.Collection.Last().Id;
                candidateSkill.SkillId = long.Parse(item);
                candidateSkill.Add(candidateSkill);
            }

            Console.WriteLine("Candidate has been added successfully");
            
        }

        //ViewCandidatesBySkill
        //Lists all candidates for each selected skill by the user
        public void ViewCandidatesBySkill()
        {
            Entities<Skill> skills = Context.Instance.Skill.Collection;
            Console.WriteLine("Filter Candidates by Skill(s)");
            Helper.PrintSkills(skills);
            Console.WriteLine(" or Type 0 to return to the Main Menu");
        TryAgain: string readLine = Console.ReadLine();
            if (readLine == "0")
                return;
            List<string> filterSkillIds = readLine.Split(',').ToList();
            long tempId;
            foreach (var item in filterSkillIds)
            {
                if (!long.TryParse(item, out tempId) ||
                    !skills.Select(s => s.Id).Contains(long.Parse(item)))
                {
                    Console.WriteLine($"Entered an invalid skill: {item} ");
                    Console.WriteLine($"Re-Enter a valid skill(s) from the list");
                    Helper.PrintSkills(skills);
                    goto TryAgain;
                }
            }

            Entities<Candidate> candidates = Context.Instance.Candidate.Collection;
            Entities<CandidateSkill> candidatesSkills = Context.Instance.CandidateSkill.Collection;

            foreach (var item in filterSkillIds)
            {
                Console.WriteLine($"Skill Name  {string.Join(",", skills.Where(s => s.Id == long.Parse(item)).Select(s2 => s2.Name))} ");
                List<string> skillCandidatesNames = new List<string>();
                List<CandidateSkill> candidateSkills = candidatesSkills.Where(c => c.SkillId == long.Parse(item)).ToList();
                foreach (var candidate in candidateSkills)
                {
                    skillCandidatesNames.AddRange(candidates.Where(s => s.Id == candidate.CandidateId).Select(s2 => $"{s2.FirstName} {s2.LastName}"));
                }
                Console.WriteLine($"Candidates: {String.Join(",", skillCandidatesNames)}");
            }
        }

        //Add(Candidate)
        //Insert the actual candidate recocrd into the DB
        public bool Add(Candidate candidate)
        {
            return Context.Instance.Candidate.Insert(candidate);
        }
        #endregion
    }
}
