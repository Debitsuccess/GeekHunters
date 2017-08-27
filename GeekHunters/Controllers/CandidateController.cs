using GeekHunters.Interfaces;
using GeekHunters.Models;
using GeekHunters.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeekHunters.Controllers
{
    [Route("api/[controller]")]
    public class CandidateController : Controller, ICandidateController
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCandidatesAsynch()
        {
            var candidates = await DocumentDBRepository<Candidate>.GetItemsAsync(c => true);
            return new ObjectResult(candidates);
        }
        
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCandidateAsynch(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            Candidate candidate = await DocumentDBRepository<Candidate>.GetItemAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return new ObjectResult(candidate);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateAsync([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await DocumentDBRepository<Candidate>.UpdateItemAsync(candidate.ID.ToString(), candidate);
            return new OkResult();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsynch([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await DocumentDBRepository<Candidate>.CreateItemAsync(candidate);
            return new ObjectResult(candidate);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteAsynch(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var candidate = await DocumentDBRepository<Candidate>.GetItemAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            await DocumentDBRepository<Candidate>.DeleteItemAsync(candidate.ID.ToString());

            return Ok(id);
        }
    }
}
