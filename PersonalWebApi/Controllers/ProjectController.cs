using PersonalWeb.BusinessLayer.DTO.ProjectDTO;
using PersonalWeb.BusinessLayer.Services.Authorize.AdminRep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class ProjectController : ControllerBase
    {
        private readonly IAdminPanelService _adminService;
        public ProjectController(IAdminPanelService adminService)
        {
            _adminService = adminService;
        }

        [Authorize]
        [HttpPost("CreateNewProject")]
        public async Task<IActionResult> CreateNewProject([FromForm] CreateProject projectDto)
        {
            var load = _adminService.CreateNewProject(projectDto);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpPut("UpdateProjectById")]
        public async Task<IActionResult> UpdateProjectById([FromForm] UpdateProjectDto projectDto)
        {
            var load = _adminService.UpdateProjectById(projectDto);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpDelete("DeleteProjectById")]
        public async Task<IActionResult> DeleteProjectById(int projectId)
        {
            var load = _adminService.DeleteProjectById(projectId);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("GetProjectById")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            var load = _adminService.GetProjectById(projectId);

            if (load.StatusCode == 200)
                return Ok(load.ProjectInfo);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("GetProjectList")]
        public async Task<IActionResult> GetProjectList(int currentPage, int take, string? type, bool? asc = true, string columnName = "CreateDate")
        {
            var load = _adminService.GetProjectList(currentPage, take, asc, columnName, type);

            if (load.StatusCode == 200)
                return Ok(new { List = load.ProjectList, AllRecords = load.All });

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }
    }
}
