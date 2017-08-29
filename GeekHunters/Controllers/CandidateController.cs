using GeekHunters.Interfaces;
using GeekHunters.Models;
using GeekHunters.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeekHunters.Controllers
{
    [Route("api/[controller]")]
    public class CandidateController : Controller
    {
        private readonly ICandidateService iCandidateService = null;
        private readonly CandidateService candidateService = null;

        public CandidateController()
        {
            candidateService = new CandidateService(iCandidateService);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCandidatesAsynch()
        {
            var candidates = await candidateService.GetAllAsync();
            return new ObjectResult(candidates);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCandidateAsynch(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            Candidate candidate = await candidateService.GetAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return new ObjectResult(candidate);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsynch([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await candidateService.AddAsync(candidate);
            return new ObjectResult(candidate);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateAsync([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await candidateService.UpdateAsync(candidate.ID.ToString(), candidate);
            return new OkResult();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteAsynch(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var candidate = await candidateService.GetAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            await candidateService.DeleteAsync(candidate.ID.ToString());

            return Ok(id);
        }
    }
}
