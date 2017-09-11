using System;
using System.Collections;
using System.Collections.Generic;
using GeekHunter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestGeekHunter
{
	[TestClass]
	public class DataProviderTests
	{
		[TestMethod]
		public void GetAllSkillsTestReturnsTwoSkills() {
			//Arrange
			var mockRepository = new Mock<ISQLiteProcessor>();

			List<Skill> testSkills = new List<Skill> {new Skill {Id = 1, Name = "TFS", IsSelected = true}, new Skill {Id = 2, Name = "git", IsSelected = false} }; 

			mockRepository.Setup(x => x.GetAllSkills()).Returns(testSkills);

			var dataService = new DataService(mockRepository.Object);

			//Act
			var mySkills = dataService.GetAllSkills();

			//Assert
			mockRepository.VerifyAll();
			Assert.IsNotNull(mySkills);
			Assert.AreEqual(mySkills.Count, 2);
			Assert.AreEqual(mySkills[0].Name, "TFS");
		}

		[TestMethod]
		public void GetAllCandidatesTestReturnsTwoCandidates() {
			//Arrange
			var mockRepository = new Mock<ISQLiteProcessor>();

			List<Candidate> testCandidates = new List<Candidate> { new Candidate { Id = 1, FirstName = "Marcella", LastName = "Baratheon", SkillIds = new List<int> {1,2}, SkillNames = "Drawing, Painting"}};
			Dictionary<int, string> candidateSkills = new Dictionary<int, string>();
			candidateSkills.Add(1,"Drawing");
			candidateSkills.Add(2,"Painting");
			mockRepository.Setup(x => x.GetAllCandidates()).Returns(testCandidates);
			mockRepository.Setup(x => x.GetCandidateSkills(1)).Returns(candidateSkills);

			var dataService = new DataService(mockRepository.Object);

			//Act
			var myCandidates = dataService.GetAllCandidates();

			//Assert
			mockRepository.VerifyAll();
			Assert.IsNotNull(myCandidates);
			Assert.AreEqual(myCandidates.Count, 1);
			Assert.AreEqual(myCandidates[0].FirstName, "Marcella");
			Assert.AreEqual(myCandidates[0].SkillIds.Count, 2);
		}

		[TestMethod]
		public void CreateCandidateReturnsNextCandidateId() {
			//Arrange
			var mockRepository = new Mock<ISQLiteProcessor>();

			List<int> testSkillIds = new List<int> {1,2,3};

			mockRepository.Setup(x => x.CreateCandidateAndSkills("Khal", "Drogo", new List<int> {1, 2, 3})).Returns(99);

			var dataService = new DataService(mockRepository.Object);

			//Act
			var newCandidateId = dataService.CreateCandidate("Khal", "Drogo", new List<int> { 1, 2, 3 });

			//Assert
			mockRepository.VerifyAll();
			Assert.IsNotNull(newCandidateId);
			Assert.AreEqual(99, newCandidateId);
		}

	}
}
