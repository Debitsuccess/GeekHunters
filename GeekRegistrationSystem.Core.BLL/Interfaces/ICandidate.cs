using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekRegistrationSystem.Core.BLL.Interfaces
{
    public interface ICandidate
    {
        void ViewAllCandidates();
        void AddCandidate();
        bool Add(Candidate candidate);
        void ViewCandidatesBySkill();
    }
}
