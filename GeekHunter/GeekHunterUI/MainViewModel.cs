using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekHunter;

namespace GeekHunterUI
{
	public class MainViewModel
	{
		private DataService dataServivce;

		public MainViewModel()
		{
			dataServivce = new DataService(new SQLiteProcessor());
			Skills = new ObservableCollection<Skill>();
			Candidates = new ObservableCollection<Candidate>();
			dataServivce.GetAllSkills().ToList().ForEach(skill => Skills.Add(skill));

			ReloadAllCandidates();
		}
		public ObservableCollection<Skill> Skills { get; private set; }
		
		internal void AddCandidate(string firstName, string lastName, List<int> skillIds)
		{
			dataServivce.CreateCandidate(firstName, lastName, skillIds);

			Candidates.Clear();
			ReloadAllCandidates();
		}

		public ObservableCollection<Candidate> Candidates { get; private set; }

		public void SearchCandidate(List<int> skillIds)
		{
			var searchResult = Candidates.Where(candidate =>
			{
				return skillIds.All(requiredSkill => candidate.SkillIds.Contains(requiredSkill));
			}).ToList();

			Candidates.Clear();

			searchResult.ToList().ForEach(candidate => Candidates.Add(candidate));
		}

		public void ReloadAllCandidates()
		{
			Candidates.Clear();
			dataServivce.GetAllCandidates().ToList().ForEach(candidate => Candidates.Add(candidate));
		}
	}
}
