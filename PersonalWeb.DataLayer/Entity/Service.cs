using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.DataLayer.Entity
{
    public class Service
    {
        #region property
        public int ServiceId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }

        #endregion

        #region relations
        #endregion
    }
}
