using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.DTO.Email
{
    public class EmailCodeDto
    {
        [Display(Name ="Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
