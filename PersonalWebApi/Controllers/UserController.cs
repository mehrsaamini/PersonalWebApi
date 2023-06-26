using PersonalWeb.BusinessLayer.Services.Authorize.AdminRep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWeb.BusinessLayer.DTO.UserDTO;

namespace PersonalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class UserController : ControllerBase
    {
        private readonly IAdminPanelService _adminService;
        public UserController(IAdminPanelService adminService)
        {
            _adminService = adminService;
        }

        [Authorize]
        [HttpGet("GetUserDetailsById")]
        public async Task<IActionResult> GetUserDetailsById(int userId)
        {
            var load = _adminService.GetUserDetailsById(userId);

            if (load.StatusCode == 200)
                return Ok(load.UserDetails);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpGet("GetUsersList")]
        public async Task<IActionResult> GetUsersList(int currentPage, int take, bool? asc = true, string columnName = "CreateDate")
        {
            var load = _adminService.GetUsersList(currentPage, take, asc, columnName);

            if (load.StatusCode == 200)
                return Ok(new { List = load.UserList, AllRecords = load.All });

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpPost("SendProjectRequest")]
        public async Task<IActionResult> SendProjectRequest(SendRequestDto requestDto)
        {
            var load = _adminService.SendProjectRequest(requestDto);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }
        
    }
}
