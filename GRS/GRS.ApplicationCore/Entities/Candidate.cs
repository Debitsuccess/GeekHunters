using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GRS.ApplicationCore.Entities
{
    public class Candidate : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CandidateSkill> Skills { get; set; } = new List<CandidateSkill>();

        public void AddSkill(int skillId)
        {
            if (!Skills.Any(i => i.SkillId == skillId))
            {
                Skills.Add(new CandidateSkill() { SkillId = skillId });                
            }            
        }
    }
}
