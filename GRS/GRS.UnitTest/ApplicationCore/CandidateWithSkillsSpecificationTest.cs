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
    public class CandidateWithSkillsSpecificationTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(5)]        
        public void Expression_ExistingId_ReturnsExpectedObject(int id)
        {
            //Arrange
            var spec = new CandidateWithSkillsSpecification(id);

            // Act
            var result = GetTestItemCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            //Assert
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(10)]        
        public void Expression_NonExistingId_ReturnsNothing(int id)
        {
            //Arrange
            var spec = new CandidateWithSkillsSpecification(id);

            // Act
            var result = GetTestItemCollection()
                .AsQueryable()
                .Any(spec.Criteria);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        public List<Candidate> GetTestItemCollection()
        {
            return new List<Candidate>()
            {
                new Candidate() { Id = 1, FirstName = "Matt1", LastName = "Duck1" },
                new Candidate() { Id = 2, FirstName = "Matt2", LastName = "Duck2" },
                new Candidate() { Id = 3, FirstName = "Matt3", LastName = "Duck3" },
                new Candidate() { Id = 4, FirstName = "Matt4", LastName = "Duck4" },
                new Candidate() { Id = 5, FirstName = "Matt5", LastName = "Duck5" }
            };
        }
    }
}
