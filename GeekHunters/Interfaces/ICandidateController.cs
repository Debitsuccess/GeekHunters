using GeekHunters.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeekHunters.Interfaces
{
    public interface ICandidateController
    {
        Task<IActionResult> GetCandidatesAsynch();
        Task<IActionResult> GetCandidateAsynch(string id);
        Task<ActionResult> UpdateAsync(Candidate candidate);
        Task<IActionResult> AddAsynch(Candidate candidate);
        Task<IActionResult> DeleteAsynch(string id);
    }
}
