using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.DataLayer.Entity
{
    public class Skill
    {
        #region property
        public int SkillId { get; set; }

        public string Name { get; set; }

        public int Percent { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }
        #endregion

        #region relations
        #endregion
    }
}
