using GeekRegistrationSystem.Core.BLL.Interfaces;
using GeekRegistrationSystem.Core.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekRegistrationSystem.Core.BLL.Models
{
    public class CandidateSkill : IEntity<CandidateSkill>, ICandidateSkill
    {
        #region Table Fields

        [DisplayName("Identity")]
        [Category("Column")]
        [DataObjectFieldAttribute(true, true, false)]//Primary key attribute 
        public long Id { get; set; }

        [DisplayName("SkillId")]
        [Category("Column")]
        public long SkillId { get; set; }

        [DisplayName("CandidateId")]
        [Category("Column")]
        public long CandidateId { get; set; }
        #endregion

        #region Initialize
        public CandidateSkill() { }
        #endregion
        #region Implementations
        //Add(CandidateSkill)
        //Inserts a CandidateSkill record into the DB
        public bool Add(CandidateSkill candidateSkill)
        {
            return Context.Instance.CandidateSkill.Insert(candidateSkill);
        }
        #endregion
    }
}
