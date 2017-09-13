using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekHunter
{
	public interface ISQLiteProcessor
	{
		int CreateCandidateAndSkills(string firstName, string lastName, IList<int> skillIds);
		Dictionary<int, string> GetCandidateSkills(int candidateId);
		IList<Candidate> GetAllCandidates();
		IList<Skill> GetAllSkills();
	}

	public class SQLiteProcessor : ISQLiteProcessor
	{
		private string sqliteAddress;

		public SQLiteProcessor(string sqliteAddress = @"Data Source=C:\Users\Joe\Documents\GitHub\GeekHunters\GeekHunter.sqlite")
		{
			this.sqliteAddress = sqliteAddress;
		}

		public int CreateCandidateAndSkills(string firstName, string lastName, IList<int> skillIds)
		{
			var candidateId = GetAllCandidates().Max(c => c.Id) + 1;
			CreateCandidate(candidateId, firstName, lastName);
			foreach (int skillId in skillIds)
			{
				CreateCandidateSkills(candidateId, skillId);
				
			}
			return candidateId;
		}

		private void ExecuteNonQuery(string query) {
			using (SQLiteConnection conn = new SQLiteConnection(sqliteAddress)) {
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand(conn)) {
					cmd.CommandText = query;
					cmd.ExecuteNonQuery();
				}
			}
		}

		private void CreateCandidate(int id, string firstName, string LastName)
		{
			var query = $"insert into Candidate(id, firstname, lastname) values({id}, '{firstName}', '{LastName}')";

			ExecuteNonQuery(query);
		}

		private void CreateCandidateSkills(int Candidateid, int SkillId) {
			var query = $"insert into CandidateSkill(CandidateId, SkillId) values({Candidateid}, {SkillId})";
			ExecuteNonQuery(query);
		}


		public Dictionary<int, string> GetCandidateSkills(int candidateId) {
			using (SQLiteConnection conn = new SQLiteConnection(sqliteAddress)) {
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand(conn)) {
					cmd.CommandText = $"select id, name from CandidateSkill join Skill on skill.Id = CandidateSkill.SkillId where CandidateId = {candidateId}";
					using (SQLiteDataReader reader = cmd.ExecuteReader()) {
						Dictionary<int, string> skills = new Dictionary<int, string>();
						while (reader.Read()) {
							var id = reader.GetInt32(0);
							var name = reader.GetString(1);

							skills.Add(id, name);
						}
						return skills;
					}
				}
			}
		}

		public IList<Candidate> GetAllCandidates() {
			using (SQLiteConnection conn = new SQLiteConnection(sqliteAddress))
			{
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand(conn))
				{
					cmd.CommandText = "select id, FirstName, LastName from Candidate";
					using (SQLiteDataReader reader = cmd.ExecuteReader()) {
						IList<Candidate> candidates = new List<Candidate>();
						while (reader.Read()) {
							candidates.Add(new Candidate { Id = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2) });
						}
						return candidates;
					}
				}
			}
		}

		public IList<Skill> GetAllSkills() {
			using (SQLiteConnection conn = new SQLiteConnection(sqliteAddress)) {
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand(conn)) {
					cmd.CommandText = "select Id, Name from Skill";
					using (SQLiteDataReader reader = cmd.ExecuteReader()) {
						IList<Skill> candidates = new List<Skill>();
						while (reader.Read()) {
							candidates.Add(new Skill { Id = reader.GetInt32(0), Name = reader.GetString(1) });
						}
						return candidates;
					}
				}
			}
		}
	}
}
