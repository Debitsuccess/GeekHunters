using GeekHunters.Interfaces;
using GeekHunters.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeekHunters.UnitTest
{
    public class MockCandidateController : ICandidateController
    {
        public Task<IActionResult> IActionResultToReturn { get; set; }
        public Task<ActionResult> ActionResultToReturn { get; set; }

        public Task<IActionResult> GetCandidatesAsynch()
        {
            return IActionResultToReturn;
        }

        public Task<IActionResult> GetCandidateAsynch(string id)
        {
            return IActionResultToReturn;
        }

        public Task<ActionResult> UpdateAsync(Candidate candidate)
        {
            return ActionResultToReturn;
        }

        public Task<IActionResult> AddAsynch(Candidate candidate)
        {
            return IActionResultToReturn;
        }

        public Task<IActionResult> DeleteAsynch(string id)
        {
            return IActionResultToReturn;
        }
    }
}