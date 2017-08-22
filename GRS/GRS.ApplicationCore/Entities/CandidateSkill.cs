using System;
using System.Collections.Generic;
using System.Text;

namespace GRS.ApplicationCore.Entities
{
    public class CandidateSkill : BaseEntity
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }        
    }
}
