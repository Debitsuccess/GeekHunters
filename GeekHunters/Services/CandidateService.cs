using GeekHunters.Interfaces;
using System.Collections.Generic;
using GeekHunters.Models;
using GeekHunters.Repository;
using System.Threading.Tasks;
using System;

namespace GeekHunters.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateService _service;

        public CandidateService(ICandidateService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            var candidates = await DocumentDBRepository<Candidate>.GetItemsAsync(c => true);
            return candidates;
        }

        public async Task<Candidate> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Candidate ID cannot be empty");
            }

            var candidate = await DocumentDBRepository<Candidate>.GetItemAsync(id);

            return candidate;
        }

        public async Task<Candidate> AddAsync(Candidate candidate)
        {
            if (candidate == null)
            {
                throw new Exception("Candidate cannot be null");
            }

            if (String.IsNullOrEmpty(candidate.FirstName))
            {
                throw new Exception("First name cannot be empty");
            }

            await DocumentDBRepository<Candidate>.CreateItemAsync(candidate);

            return candidate;
        }

        public async Task UpdateAsync(string id, Candidate candidate)
        {
            await DocumentDBRepository<Candidate>.UpdateItemAsync(id, candidate);
        }

        public async Task DeleteAsync(string id)
        {
            await DocumentDBRepository<Candidate>.DeleteItemAsync(id);
        }
    }
}
