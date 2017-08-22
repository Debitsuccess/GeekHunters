using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GRS.Web.ViewModels
{
    public class CandidateViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int[] Skills { get; set; } = new int[] { };

        public IEnumerable<SelectListItem> AllSkills { get; set; }
    }
}
