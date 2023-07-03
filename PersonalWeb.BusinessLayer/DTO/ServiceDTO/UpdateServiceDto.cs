using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.DTO.ServiceDTO
{
    public class UpdateServiceDto
    {
        public int ServiceId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string IconId { get; set; }
    }
}
