using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GRS.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GRS.Web.ViewModels;
using GRS.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GRS.Web.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private readonly ICandidateService _candidateService;

        public AgentController(ICandidateService candidateService) 
            => _candidateService = candidateService;

        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Index(int skillId = 0)
        {
            ViewBag.Skills = await _candidateService.GetSkills();
            ViewBag.SelectedSkillId = skillId;
            return View(await _candidateService.GetCandidates(skillId));
        }

        public async Task<ViewResult> Create()
        {
            CandidateViewModel candidateViewModel = new CandidateViewModel();
            candidateViewModel.AllSkills = await GetAllSkills();
            return View("Edit", candidateViewModel);
        }

        public async Task<ViewResult> Edit(int id)
        {
            CandidateViewModel candidateViewModel;

            if (id == 0)            
                candidateViewModel = new CandidateViewModel();
            else
                candidateViewModel = await _candidateService.GetCandidate(id);

            candidateViewModel.AllSkills = await GetAllSkills();
            return View(candidateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CandidateViewModel candidateViewModel)
        {
            if (ModelState.IsValid)
            {
                await _candidateService.SaveCandidate(candidateViewModel);

                if(TempData != null)
                    TempData["message"] = $"Candidate {candidateViewModel.FirstName + " " + candidateViewModel.LastName } record has been saved";

                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(candidateViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _candidateService.DeleteCandidate(id);

            if(TempData != null)
                TempData["message"] = $"Record was deleted";            

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetAllSkills()
        {
            var list = (await _candidateService.GetSkills());
            var result = new List<SelectListItem>();

            foreach (var item in list)
            {
                result.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            return result;
        }
    }
}