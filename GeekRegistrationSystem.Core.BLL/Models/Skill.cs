using GeekRegistrationSystem.Core.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekRegistrationSystem.Core.BLL.Models
{
    public class Skill : IEntity<Skill>
    {
        #region Table Fields

        [DisplayName("Identity")]
        [Category("Column")]
        [DataObjectFieldAttribute(true, true, false)]//Primary key attribute 
        public long Id { get; set; }

        [DisplayName("Name")]
        [Category("Column")]
        public string Name { get; set; }
        #endregion

        #region Initialize
        public Skill() { }
        #endregion
        
    }
}
