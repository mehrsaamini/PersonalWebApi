using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.DataLayer.Entity
{
    public class Project
    {
        #region property
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string? Link { get; set; }

        public string Type { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }

        #endregion

        #region relations
        #endregion
    }
}
