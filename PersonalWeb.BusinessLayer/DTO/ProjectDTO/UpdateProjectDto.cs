using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.DTO.ProjectDTO
{
    public class UpdateProjectDto
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public IFormFile? Picture { get; set; }

        public string? Link { get; set; }

        public string Type { get; set; }
    }
}
