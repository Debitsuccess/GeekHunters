using GRS.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GRS.Web.ViewModels;
using GRS.ApplicationCore.Interfaces;
using GRS.ApplicationCore.Entities;
using GRS.ApplicationCore.Specifications;

namespace GRS.Web.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<CandidateSkill> _skillRepository;
        private readonly IRepository<Skill> _skillListRepository;

        public CandidateService(IRepository<Candidate> candidateRepository, 
                IRepository<CandidateSkill> skillRepository,
                IRepository<Skill> skillListRepository)
        {
            _candidateRepository = candidateRepository;
            _skillRepository = skillRepository;
            _skillListRepository = skillListRepository;
        }

        public async Task<List<CandidateViewModel>> GetCandidates(int skillId)
        {
            if (skillId == 0)
            {
                var candidates = _candidateRepository.List();
                return candidates.Select(c => GetCandidateViewModel(c)).ToList();
            }

            var candidateViewModels = new List<CandidateViewModel>();
            var candidateHash = new Dictionary<int, bool>();
            var skillSpec = new SkillsFilterSpecification(skillId);
            var skills = _skillRepository.List(skillSpec);

            foreach (var skill in skills)
            {
                var candidateId = skill.CandidateId;

                if (candidateHash.ContainsKey(candidateId))
                    continue;

                candidateHash.Add(candidateId, true);
                candidateViewModels.Add(await GetCandidate(candidateId));
            }

            return candidateViewModels;
        }

        public async Task<CandidateViewModel> GetCandidate(int id)
        {            
            var candidate = await LoadCandidateAndSkills(id);
            return GetCandidateViewModel(candidate);
        }

        public async Task<List<Skill>> GetSkills() => _skillListRepository.List();

        public async Task SaveCandidate(CandidateViewModel candidateViewModel)
        {
            Candidate candidate = null;

            if (candidateViewModel.Id == 0)
            {
                candidate = new Candidate()
                {
                    Id = candidateViewModel.Id,
                    FirstName = candidateViewModel.FirstName,
                    LastName = candidateViewModel.LastName
                };

                foreach (var skillId in candidateViewModel.Skills)
                    candidate.AddSkill(skillId);


                _candidateRepository.Add(candidate);
            }
            else
            {
                candidate = await LoadCandidateAndSkills(candidateViewModel.Id);
                candidate.FirstName = candidateViewModel.FirstName;
                candidate.LastName = candidateViewModel.LastName;

                foreach (var skillId in candidateViewModel.Skills)
                    candidate.AddSkill(skillId);

                foreach (var skill in candidate.Skills.ToArray())
                {
                    if (!candidateViewModel.Skills.Any(i => i == skill.SkillId))
                    {                
                        candidate.Skills.Remove(skill);
                    }
                }

                _candidateRepository.Update(candidate);                
            }
        }

        public async Task DeleteCandidate(int id)
        {
            var candidate = await LoadCandidateAndSkills(id);
            _candidateRepository.Delete(candidate);
        }

        private CandidateViewModel GetCandidateViewModel(Candidate candidate)
        {
            var candidateViewModel = new CandidateViewModel()
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Skills = candidate.Skills.Select(s => s.SkillId).ToArray()
            };

            return candidateViewModel;
        }

        private  async Task<Candidate> LoadCandidateAndSkills(int id)
        {
            var candidateSpec = new CandidateWithSkillsSpecification(id);
            return _candidateRepository.List(candidateSpec).FirstOrDefault();
        }
    }
}
