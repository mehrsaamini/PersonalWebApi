using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.DTO.ServiceDTO
{
    public class CreateService
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile Icon { get; set; }
    }
}
