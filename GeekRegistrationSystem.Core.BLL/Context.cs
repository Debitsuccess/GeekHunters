using GeekRegistrationSystem.Core.BLL.Models;

namespace GeekRegistrationSystem.Core.BLL
{
    public class Context
    {
        #region Class field and properties

        #region Singleton
        private static Context _instance = null;
        /// <summary>
        /// Singleton context
        /// </summary>
        public static Context Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Context();
                return _instance;
            }
        }
        #endregion

        #region Model
        private Candidate _candidate = null;
        public Candidate Candidate
        {
            get
            {
                _candidate = new Candidate();
                _candidate.Load();
                return _candidate;
            }
        }

        private Skill _skill = null;
        public Skill Skill
        {
            get
            {
                _skill = new Skill();
                _skill.Load();
                return _skill;
            }
        }

        private CandidateSkill _candidateSkill = null;
        public CandidateSkill CandidateSkill
        {
            get
            {
                _candidateSkill = new CandidateSkill();
                _candidateSkill.Load();
                return _candidateSkill;
            }
        }
        #endregion

        #endregion

        #region Initialize
        private Context() { }
        #endregion

    }
}
