﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.DTO.Email
{
    public class VerifyEmailDto
    {
        [Display(Name ="EmailAddress")]
        [EmailAddress]
        public string Email { get; set; }
        public string EmailCode { set; get; }
    }
}
