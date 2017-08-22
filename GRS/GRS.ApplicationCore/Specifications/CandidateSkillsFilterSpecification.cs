using GRS.ApplicationCore.Entities;
using GRS.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GRS.ApplicationCore.Specifications
{
    public class CandidateSkillsFilterSpecification : ISpecification<CandidateSkill>
    {
        public CandidateSkillsFilterSpecification(int candidateId) => CandidateId = candidateId;

        public int CandidateId { get; }

        public Expression<Func<CandidateSkill, bool>> Criteria => c => c.CandidateId == CandidateId;

        public List<Expression<Func<CandidateSkill, object>>> Includes { get; } = new List<Expression<Func<CandidateSkill, object>>>();

        public void AddInclude(Expression<Func<CandidateSkill, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
