using GRS.ApplicationCore.Entities;
using GRS.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace GRS.ApplicationCore.Specifications
{
    public class SkillsFilterSpecification : ISpecification<CandidateSkill>
    {
        public SkillsFilterSpecification(int skillId) => SkillId = skillId;

        public int SkillId { get; }

        public Expression<Func<CandidateSkill, bool>> Criteria => c => c.SkillId == SkillId;

        public List<Expression<Func<CandidateSkill, object>>> Includes { get; } = new List<Expression<Func<CandidateSkill, object>>>();

        public void AddInclude(Expression<Func<CandidateSkill, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }        
    }
}
