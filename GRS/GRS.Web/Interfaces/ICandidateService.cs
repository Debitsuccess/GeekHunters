using GRS.ApplicationCore.Entities;
using GRS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRS.Web.Interfaces
{
    public interface ICandidateService
    {
        Task<List<CandidateViewModel>> GetCandidates(int skillId);
        Task<CandidateViewModel> GetCandidate(int id);
        Task<List<Skill>> GetSkills();
        Task SaveCandidate(CandidateViewModel candidateViewModel);
        Task DeleteCandidate(int id);
    }
}
