using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using GeekRegistrationSystem.Common;
using GeekRegistrationSystem.Core.BLL;
using GeekRegistrationSystem.Core.BLL.Models;
using GeekRegistrationSystem.Core.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeekRegistrationSystem.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
 
        [TestMethod]
        public void AddCandidate()
        {
            #region Initialize DataAccessAdapter
            DatabaseService.Initialize(ConfigurationManager.ConnectionStrings["GeekRegistrationConnectionStringTest"].ConnectionString);
            #endregion


            Candidate candidate = new Candidate();
            candidate.FirstName = "First Name";
            candidate.LastName = "Last Name";
            Add(candidate);

            List<long> filterSkillIds = new List<long>();
            filterSkillIds.Add(1);
            filterSkillIds.Add(2);

            foreach (var item in filterSkillIds)
            {
                CandidateSkill candidateSkill = new CandidateSkill();
                candidateSkill.CandidateId = Context.Instance.Candidate.Collection.Last().Id;
                candidateSkill.SkillId = item;
                candidateSkill.Add(candidateSkill);
            }

        }
        public bool Add(Candidate candidate)
        {
            return Context.Instance.Candidate.Insert(candidate);
        }

        [TestMethod]
        public void ViewAllCandidates()
        {
            #region Initialize DataAccessAdapter
            DatabaseService.Initialize(ConfigurationManager.ConnectionStrings["GeekRegistrationConnectionStringTest"].ConnectionString);
            #endregion

            Candidate candidate = new Candidate();
            candidate.ViewAllCandidates();
        }

        [TestMethod]
        public void ViewCandidatesBySkill()
        {
            #region Initialize DataAccessAdapter
            DatabaseService.Initialize(ConfigurationManager.ConnectionStrings["GeekRegistrationConnectionStringTest"].ConnectionString);
            #endregion

            Entities<Skill> skills = Context.Instance.Skill.Collection;
            Console.WriteLine("Filter Candidates by Skill(s)");
            Helper.PrintSkills(skills);
            Console.WriteLine(" or Type 0 to return to the Main Menu");
            List<string> filterSkillIds = new List<string>();
            filterSkillIds.Add("1");
            filterSkillIds.Add("2");
            long tempId;
            foreach (var item in filterSkillIds)
            {
                if (!long.TryParse(item, out tempId) ||
                    !skills.Select(s => s.Id).Contains(long.Parse(item)))
                {
                    Console.WriteLine($"Entered an invalid skill: {item} ");
                    Console.WriteLine($"Re-Enter a valid skill(s) from the list");
                    Helper.PrintSkills(skills);
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
    }
}
