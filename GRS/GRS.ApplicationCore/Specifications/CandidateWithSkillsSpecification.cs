using GRS.ApplicationCore.Entities;
using GRS.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace GRS.ApplicationCore.Specifications
{
    public class CandidateWithSkillsSpecification : ISpecification<Candidate>
    {
        public CandidateWithSkillsSpecification(int id)
        {
            Id = id;
            AddInclude(c => c.Skills);
        }

        public int Id { get; }

        public Expression<Func<Candidate, bool>> Criteria => 
            c => c.Id == Id;

        public List<Expression<Func<Candidate, object>>> Includes { get; } = new List<Expression<Func<Candidate, object>>>();

        public void AddInclude(Expression<Func<Candidate, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
