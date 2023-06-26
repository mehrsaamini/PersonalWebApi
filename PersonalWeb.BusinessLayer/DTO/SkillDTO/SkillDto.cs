using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.DTO.SkillDTO
{
    public class SkillDto
    {
        public int SkillId { get; set; }

        public string Name { get; set; }

        public int Percent { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }

    }
}
