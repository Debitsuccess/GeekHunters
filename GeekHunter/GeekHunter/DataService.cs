using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekHunter
{
	public class DataService
	{
		private readonly ISQLiteProcessor sqLiteProcessor;

		public DataService(ISQLiteProcessor sqLiteProcessor)
		{
			this.sqLiteProcessor = sqLiteProcessor;
		}

		public IList<Candidate> GetAllCandidates()
		{
			var candidates = sqLiteProcessor.GetAllCandidates();
			foreach (var candidate in candidates)
			{
				var skills = sqLiteProcessor.GetCandidateSkills(candidate.Id);

				List<int> ids = skills.Keys.ToList();
				List<string> names = skills.Values.ToList();

				candidate.SkillNames = string.Join(",", names);
				candidate.SkillIds = ids;
			}
			return candidates;
		}

		public IList<Skill> GetAllSkills() {
			return sqLiteProcessor.GetAllSkills();
		}

		public int CreateCandidate(string firstName, string lastName, IList<int> skillIds )
		{
			return sqLiteProcessor.CreateCandidateAndSkills(firstName, lastName, skillIds);
		}
	}
}
