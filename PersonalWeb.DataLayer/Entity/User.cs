using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.DataLayer.Entity
{
    public class User
    {
        #region property
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Family { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string EmailCode { get; set; }
        public bool AccessAdminPanel { get; set; }
        public string? ProjectDescription { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public DateTime CreateDate { get; set; }
        #endregion
    }
}
