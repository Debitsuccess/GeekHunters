using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;



namespace GeekHunter
{
	class Program
	{
		static void Main(string[] args) {
			var path = "GeekHunter.sqlite";

			var dataService = new DataService(new SQLiteProcessor());

			IList<Candidate> candidates;
			candidates = dataService.GetAllCandidates();

			IList<Skill> skills;
			skills = dataService.GetAllSkills();

			foreach (var candidate in candidates) {
				Console.WriteLine(candidate.FirstName + " " + candidate.LastName + " " + candidate.SkillNames);
			}

			foreach (var skill in skills) {
				Console.WriteLine(skill.Id + " " + skill.Name);
			}
			Console.ReadKey();

			dataService.CreateCandidate("Jane", "Doe", new List<int> {1, 2, 3});
		}
	}
}
