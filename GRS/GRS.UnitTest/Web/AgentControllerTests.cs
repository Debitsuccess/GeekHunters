using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using GRS.ApplicationCore.Interfaces;
using GRS.ApplicationCore.Entities;
using GRS.ApplicationCore.Specifications;
using GRS.Web.Services;
using GRS.Web.Interfaces;
using GRS.Web.Controllers;
using GRS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GRS.UnitTest
{
    [TestFixture]
    public class AgentControllerTests
    {
        private ICandidateService _candidateService;
        Mock<IRepository<Candidate>> candidateRepositoryMock;
        Mock<IRepository<CandidateSkill>> skillRepositoryMock ;
        Mock<IRepository<Skill>> skillListRepositoryMock;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            candidateRepositoryMock = GetCandidateRepository();
            skillRepositoryMock = GetSkillRepository();
            skillListRepositoryMock = GetSkillListRepository();

            _candidateService = 
                new CandidateService(candidateRepositoryMock.Object, skillRepositoryMock.Object, skillListRepositoryMock.Object);
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(1, 2)]
        [TestCase(3, 1)]
        [TestCase(5, 0)]
        public void Index_FilterOnSkill_ReturnsExpectedCount(int skillId, int resultCount)
        {
            // Arrange
            AgentController target = new AgentController(_candidateService);

            // Act            
            List<CandidateViewModel> result
               = GetViewModel<List<CandidateViewModel>>(target.Index(skillId).Result);

            //Assert
            Assert.That(result.Count, Is.EqualTo(resultCount));
        }

        [Test]
        public async Task Edit_CanEditNewCandidate_ReturnsExpectedObject()
        {
            // Arrange
            var callArgs = new List<Candidate>();
            AgentController target = new AgentController(_candidateService);
            var candidateViewModel = new CandidateViewModel
            {
                FirstName = "JohnFirstName",
                LastName = "JohnLastName"
            };

            candidateRepositoryMock.Setup(r => r.Add(It.IsAny<Candidate>()))
                .Callback((Candidate s) => callArgs.Add(s));

            // Act
            await target.Edit(candidateViewModel);

            // Assert            
            Assert.That(1, Is.EqualTo(callArgs.Count));
            Assert.That(candidateViewModel.FirstName, Is.EqualTo(callArgs[0].FirstName));
            Assert.That(candidateViewModel.LastName, Is.EqualTo(callArgs[0].LastName));
        }

        [Test]
        public async Task Edit_CanEditNewWithSkillsCandidate_ReturnsExpectedObject()
        {
            // Arrange
            var callArgs = new List<Candidate>();
            AgentController target = new AgentController(_candidateService);
            var candidateViewModel = new CandidateViewModel
            {
                FirstName = "JohnFirstName",
                LastName = "JohnLastName"
            };

            candidateViewModel.Skills = new int[] { 1, 2 };
            
            candidateRepositoryMock.Setup(r => r.Add(It.IsAny<Candidate>()))
                .Callback((Candidate s) => callArgs.Add(s));

            // Act
            await target.Edit(candidateViewModel);

            // Assert            
            Assert.That(1, Is.EqualTo(callArgs.Count));
            Assert.That(candidateViewModel.FirstName, Is.EqualTo(callArgs[0].FirstName));
            Assert.That(candidateViewModel.LastName, Is.EqualTo(callArgs[0].LastName));
            Assert.That(candidateViewModel.Skills.Length, Is.EqualTo(callArgs[0].Skills.Count));            
        }


        [Test]
        public async Task Edit_CanEditExistingCandidate_ReturnsExpectedObject()
        {
            // Arrange
            var callArgs = new List<Candidate>();
            AgentController target = new AgentController(_candidateService);
            var candidateViewModel = new CandidateViewModel
            {
                Id = 5,
                FirstName = "JohnFirstName",
                LastName = "JohnLastName"
            };

            candidateRepositoryMock.Setup(r => r.Update(It.IsAny<Candidate>()))
                .Callback((Candidate s) => callArgs.Add(s));

            // Act
            await target.Edit(candidateViewModel);

            // Assert            
            Assert.That(1, Is.EqualTo(callArgs.Count));
            Assert.That(candidateViewModel.Id, Is.EqualTo(callArgs[0].Id));
            Assert.That(candidateViewModel.FirstName, Is.EqualTo(callArgs[0].FirstName));
            Assert.That(candidateViewModel.LastName, Is.EqualTo(callArgs[0].LastName));
        }

        [Test]
        public async Task Edit_CanEditExistingWithSkillsCandidate_ReturnsExpectedObject()
        {
            // Arrange
            var callArgs = new List<Candidate>();
            AgentController target = new AgentController(_candidateService);
            var candidateViewModel = new CandidateViewModel
            {
                Id = 5,
                FirstName = "JohnFirstName",
                LastName = "JohnLastName"
            };
            candidateViewModel.Skills = new int[] { 1, 2 };

            candidateRepositoryMock.Setup(r => r.Update(It.IsAny<Candidate>()))
                .Callback((Candidate s) => callArgs.Add(s));

            // Act
            await target.Edit(candidateViewModel);

            // Assert            
            Assert.That(1, Is.EqualTo(callArgs.Count));
            Assert.That(candidateViewModel.Id, Is.EqualTo(callArgs[0].Id));
            Assert.That(candidateViewModel.FirstName, Is.EqualTo(callArgs[0].FirstName));
            Assert.That(candidateViewModel.LastName, Is.EqualTo(callArgs[0].LastName));
            Assert.That(candidateViewModel.Skills.Length, Is.EqualTo(callArgs[0].Skills.Count));
        }

        [Test]
        public async Task Delete_CanDeleteCandidate_ReturnsExpectedObject()
        {
            // Arrange
            var callArgs = new List<Candidate>();
            AgentController target = new AgentController(_candidateService);
            candidateRepositoryMock.Setup(r => r.Delete(It.IsAny<Candidate>()))
                .Callback((Candidate s) => callArgs.Add(s));

            // Act
            await target.Delete(1);

            // Assert            
            Assert.That(1, Is.EqualTo(callArgs.Count));
            Assert.That(1, Is.EqualTo(callArgs[0].Id));            
        }

        private Mock<IRepository<Candidate>> GetCandidateRepository()
        {
            Mock<IRepository<Candidate>> mock = new Mock<IRepository<Candidate>>();
            var candidate = new List<Candidate>()
            {
                new Candidate() { Id = 1, FirstName = "Matt1", LastName = "Duck1" },
                new Candidate() { Id = 2, FirstName = "Matt2", LastName = "Duck2" },
                new Candidate() { Id = 3, FirstName = "Matt3", LastName = "Duck3" },
                new Candidate() { Id = 4, FirstName = "Matt4", LastName = "Duck4" },
                new Candidate() { Id = 5, FirstName = "Matt5", LastName = "Duck5" }
            };
            
            mock.Setup(m => m.List()).Returns(candidate);
            
            mock.Setup(m => m.List(It.Is<CandidateWithSkillsSpecification>(s => s.Id == 1))).Returns(
                new List<Candidate>() { new Candidate() { Id = 1, FirstName = "Matt1", LastName = "Duck1" } });
            
            mock.Setup(m => m.List(It.Is<CandidateWithSkillsSpecification>(s => s.Id == 2))).Returns(
                new List<Candidate>() { new Candidate() { Id = 2, FirstName = "Matt2", LastName = "Duck2" } });
            
            mock.Setup(m => m.List(It.Is<CandidateWithSkillsSpecification>(s => s.Id == 3))).Returns(
                new List<Candidate>() { new Candidate() { Id = 3, FirstName = "Matt3", LastName = "Duck3" } });
            
            mock.Setup(m => m.List(It.Is<CandidateWithSkillsSpecification>(s => s.Id == 4))).Returns(
                new List<Candidate>() { new Candidate() { Id = 4, FirstName = "Matt4", LastName = "Duck4" } });
            
            mock.Setup(m => m.List(It.Is<CandidateWithSkillsSpecification>(s => s.Id == 5))).Returns(
                new List<Candidate>() { new Candidate() { Id = 5, FirstName = "Matt5", LastName = "Duck5" } });

            return mock;
        }
        
        private Mock<IRepository<CandidateSkill>> GetSkillRepository()
        {
            Mock<IRepository<CandidateSkill>> mock = new Mock<IRepository<CandidateSkill>>();            
            mock.Setup(m => m.List(It.Is<SkillsFilterSpecification>(s => s.SkillId == 1))).Returns(
                new List<CandidateSkill>() {
                    new CandidateSkill() { Id = 1, CandidateId = 1, SkillId = 1 },
                    new CandidateSkill() { Id = 2, CandidateId = 2, SkillId = 1 }                    
                });
            
            mock.Setup(m => m.List(It.Is<SkillsFilterSpecification>(s => s.SkillId == 2))).Returns(
                new List<CandidateSkill>() {
                    new CandidateSkill() { Id = 3, CandidateId = 1, SkillId = 2 },
                    new CandidateSkill() { Id = 4, CandidateId = 3, SkillId = 2 }
                });

            mock.Setup(m => m.List(It.Is<SkillsFilterSpecification>(s => s.SkillId == 3))).Returns(
                new List<CandidateSkill>() {
                    new CandidateSkill() { Id = 5, CandidateId = 5, SkillId = 3 }
                });

            mock.Setup(m => m.List(It.Is<SkillsFilterSpecification>(s => s.SkillId == 5))).Returns(
                new List<CandidateSkill>());

            return mock;
        }

        private Mock<IRepository<Skill>> GetSkillListRepository()
        {            
            Mock<IRepository<Skill>> mock = new Mock<IRepository<Skill>>();
            var skills = new List<Skill>()
            {
                new Skill() { Id = 1, Name = "C#" },
                new Skill() { Id = 2, Name = "Java" },
                new Skill() { Id = 2, Name = "JavaScript" }
            };

            mock.Setup(m => m.List()).Returns(skills);
            return mock;
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
