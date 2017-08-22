using GRS.ApplicationCore.Entities;
using GRS.ApplicationCore.Specifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRS.UnitTest.ApplicationCore
{
    [TestFixture]
    public class CandidateSkillsFilterSpecificationTest
    {
        [Test]
        [TestCase(1, 3)]
        [TestCase(2, 2)]
        [TestCase(9, 0)]
        public void Expression_ExistingId_ReturnsExpectedNumberOfItems(int candidateId, int expectedCount)
        {
            //Arrange
            var spec = new CandidateSkillsFilterSpecification(candidateId);

            // Act
            var result = GetTestItemCollection()
                .AsQueryable()
                .Where(spec.Criteria);            

            //Assert
            Assert.That(result.Count(), Is.EqualTo(expectedCount));
        }

        public List<CandidateSkill> GetTestItemCollection()
        {
            return new List<CandidateSkill>()
            {
                new CandidateSkill() { Id = 1, CandidateId = 1, SkillId = 1 },
                new CandidateSkill() { Id = 2, CandidateId = 1, SkillId = 2 },
                new CandidateSkill() { Id = 3, CandidateId = 1, SkillId = 3 },
                new CandidateSkill() { Id = 4, CandidateId = 2, SkillId = 1 },
                new CandidateSkill() { Id = 5, CandidateId = 2, SkillId = 2 },
            };
        }
    }
}
