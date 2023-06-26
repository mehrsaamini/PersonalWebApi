using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.DataLayer.Entity
{
    public class GeneralSetting
    {
        #region property
        public int GeneralSettingId { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }

        #endregion

        #region relations
        #endregion
    }
}
