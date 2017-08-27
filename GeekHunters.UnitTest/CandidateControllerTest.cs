using GeekHunters.Controllers;
using GeekHunters.Models;
using GeekHunters.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekHunters.UnitTest
{
    [TestFixture]
    public class CandidateControllerTest
    {
        [OneTimeSetUp]
        public void RepositorySetUp()
        {
            DocumentDBRepository<Candidate>.Initialize();
        }

        [Test]
        public async Task UpdateAsync_ReturnsExpectedResults()
        {
            // Arrange
            var candidates = new List<Candidate>();
            CandidateController candidateController = new CandidateController();
            var candidate = new Candidate() {
                ID = Guid.Parse("2df26842-37fe-401d-a739-1da5c189d379"),
                FirstName = "Brad",
                LastName = "Pitt",
                Technologies = new string[] { "SQL", "CSharp" } };

            candidates.Add(candidate);

            // Act
            await candidateController.UpdateAsync(candidate);

            // Assert            
            Assert.That(1, Is.EqualTo(candidates.Count));
            Assert.That(candidate.FirstName, Is.EqualTo(candidates[0].FirstName));
            Assert.That(candidate.LastName, Is.EqualTo(candidates[0].LastName));
        }

        [Test]
        public async Task AddAsync_ReturnsExpectedResults()
        {
            var candidates = new List<Candidate>();
            CandidateController candidateController = new CandidateController();

            var candidate = new Candidate()
            {
                ID = Guid.NewGuid(),
                FirstName = $"FirstName {DateTime.Now.Millisecond}",
                LastName = "Pitt"
            };

            candidates.Add(candidate);

            await candidateController.AddAsynch(candidate);

            Assert.That(1, Is.EqualTo(candidates.Count));
        }

        [Test]
        public async Task DeleteAsync_ReturnsExpectedResults()
        {
            var candidates = new List<Candidate>();
            CandidateController candidateController = new CandidateController();

            var candidate = new Candidate()
            {
                LastName = "Pitt"
            };

            candidates.Add(candidate);

            await candidateController.AddAsynch(candidate);

            Assert.That(1, Is.EqualTo(candidates.Count));

        }

        [Test]
        public async Task DeleteAsync_ReturnsNotFoundResult()
        {
            CandidateController candidateController = new CandidateController();

            var candidate = new Candidate()
            {
                ID = Guid.NewGuid(),
                FirstName = $"FirstName {DateTime.Now.Millisecond}",
                LastName = "Last"
            };

            await candidateController.DeleteAsynch(Guid.NewGuid().ToString());

            Assert.That("Last", Is.EqualTo(candidate.LastName));

        }
    }
}
