using GeekHunters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekHunters.Interfaces
{
    public interface ICandidateService
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate> GetAsync(string id);
        Task<Candidate> AddAsync(Candidate candidate);
        Task UpdateAsync(string id, Candidate candidate);
        Task DeleteAsync(string id);
    }
}