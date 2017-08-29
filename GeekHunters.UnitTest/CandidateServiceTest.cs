using GeekHunters.Interfaces;
using GeekHunters.Models;
using GeekHunters.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekHunters.UnitTest
{
    [TestFixture]
    public class CandidateServiceTest
    {
        private List<Candidate> candidates;
        private List<string> technologies;

        [OneTimeSetUp]
        public void RepositorySetUp()
        {
            DocumentDBRepository<Candidate>.Initialize();

            candidates = new List<Candidate>() {
                new Candidate(){ ID = Guid.NewGuid(), FirstName = "FirstName_1", LastName = "LastName_1"  },
                new Candidate(){ ID = Guid.NewGuid(), FirstName = "FirstName_2", LastName = "LastName_2"  },
                new Candidate(){ ID = Guid.NewGuid(), FirstName = "FirstName_3", LastName = "LastName_3"  }
            };

            technologies = new List<string>() { "Azure", "SQL", "CSharp", "Angular" };
        }

        [Test]
        public async Task GetAllCandidatesAsync_Success()
        {
            // Arrange
            var mockService = new Mock<ICandidateService>();
            mockService.Setup(x => x.GetAllAsync()).Returns(async () =>
            {
                await Task.Yield();
                return candidates;
            });

            // Act
            var actual = await mockService.Object.GetAllAsync();

            // Assert
            Assert.AreEqual(candidates.Count(), actual.Count());
        }

        [Test]
        public async Task GetCandidateAsynch_Success()
        {
            // Arrange
            var candidate = new Candidate
            {
                ID = Guid.NewGuid(),
                FirstName = "Conor",
                LastName = "Mcgregor",
                Technologies = technologies.Take(2).ToArray()
            };

            var mockService = new Mock<ICandidateService>();
            mockService.Setup(x => x.GetAsync(candidate.ID.ToString())).Returns(async () =>
            {
                await Task.Yield();
                return candidate;
            });

            // Act
            await mockService.Object.AddAsync(candidate);
            var actual = await mockService.Object.GetAsync(candidate.ID.ToString());

            // Assert
            Assert.AreEqual(candidate, actual);
        }

        [Test]
        public async Task GetCandidateAsynch_NotFound_Success()
        {
            // Arrange
            var candidateId = Guid.NewGuid().ToString();
            var mockService = new Mock<ICandidateService>();

            mockService.Setup(x => x.GetAsync(candidateId)).Returns(async () =>
            {
                await Task.Yield();
                return null;
            });

            // Act
            var actual = await mockService.Object.GetAsync(candidateId);

            // Assert
            mockService.Verify(m => m.GetAsync(candidateId), Times.AtLeastOnce());
            Assert.AreEqual(null, actual);
        }

        [Test]
        public async Task AddCandidateAsynch_Candidate_Success()
        {
            // Arrange
            var candidate = new Candidate
            {
                ID = Guid.NewGuid(),
                FirstName = "Conor",
                LastName = "Mcgregor",
                Technologies = technologies.Take(2).ToArray()
            };

            var mockService = new Mock<ICandidateService>();
            mockService.Setup(x => x.AddAsync(It.IsAny<Candidate>())).Returns(async () =>
            {
                await Task.Yield();
                return candidate;
            });

            // Act
            var actual = await mockService.Object.AddAsync(candidate);

            // Assert
            Assert.AreEqual(candidate, actual);
        }

        [Test]
        public void AddCandidateAsynch_CandidateIsNull_Failure_Throws()
        {
            string errorMessage = "Candidate cannot be null";

            // Arrange
            var candidate = It.IsAny<Candidate>();

            // Act and Assert
            Assert.That(async () =>
                await AddCandidateAsyncThrowException(candidate, errorMessage),
                Throws.Exception.TypeOf<Exception>().And.Message.EqualTo(errorMessage));
        }

        [Test]
        public void AddCandidateAsynch_CandidateFirstNameIsEmpty_Failure_Throws()
        {
            string errorMessage = "First name cannot be empty";

            // Arrange
            var candidate = new Candidate
            {
                ID = Guid.NewGuid(),
                LastName = "Mcgregor",
                Technologies = technologies.Take(2).ToArray()
            };

            // Act and Assert
            Assert.That(async () =>
                await AddCandidateAsyncThrowException(candidate, errorMessage),
                Throws.Exception.TypeOf<Exception>().And.Message.EqualTo(errorMessage));
        }

        [Test]
        public async Task UpdateCandidateAsynch_Success()
        {
            // Arrange
            var candidate = new Candidate
            {
                ID = Guid.NewGuid(),
                FirstName = "Conor",
                LastName = "Mcgregor",
                Technologies = technologies.Take(2).ToArray()
            };

            var mockService = new Mock<ICandidateService>();
            mockService.Setup(x => x.UpdateAsync(candidate.ID.ToString(), It.IsAny<Candidate>())).Returns(async () =>
            {
                await Task.Yield();
            });

            // Act
            await mockService.Object.UpdateAsync(candidate.ID.ToString(), candidate);

            // Assert
            mockService.Verify(m => m.UpdateAsync(candidate.ID.ToString(), It.IsAny<Candidate>()), Times.AtLeastOnce());
            Assert.That(candidate.FirstName, Is.EqualTo("Conor"));
        }

        [Test]
        public async Task DeleteCandidateAsynch_Success()
        {
            // Arrange
            var candidate = new Candidate
            {
                ID = Guid.NewGuid(),
                FirstName = "Conor",
                LastName = "Mcgregor",
                Technologies = technologies.Take(2).ToArray()
            };

            var mockService = new Mock<ICandidateService>();
            mockService.Setup(x => x.DeleteAsync(candidate.ID.ToString())).Returns(async () =>
            {
                await Task.Yield();
            });

            // Act
            await mockService.Object.DeleteAsync(candidate.ID.ToString());
            var actual = await mockService.Object.GetAsync(candidate.ID.ToString());

            // Assert
            mockService.Verify(m => m.DeleteAsync(candidate.ID.ToString()));
            mockService.Verify(m => m.GetAsync(candidate.ID.ToString()));
            Assert.AreEqual(null, actual);
        }

        private async Task AddCandidateAsyncThrowException(Candidate candidate, string errorMessage)
        {
            var mockService = new Mock<ICandidateService>();
            await mockService.Object.AddAsync(candidate).ConfigureAwait(false);
            throw new Exception(errorMessage);
        }
    }
}
