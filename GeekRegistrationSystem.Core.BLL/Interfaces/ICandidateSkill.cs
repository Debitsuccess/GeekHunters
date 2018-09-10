using GeekRegistrationSystem.Core.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekRegistrationSystem.Core.BLL.Interfaces
{
    public interface ICandidateSkill
    {
        bool Add(CandidateSkill candidateSkill);
    }
}
