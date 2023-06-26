using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.DTO.UserDTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ProjectDescription { get; set; }
    }
}
