using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekHunter
{
	public class Candidate
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public List<int> SkillIds { get; set; }
		public string SkillNames { get; set; }
	}


}
