using PersonalWeb.BusinessLayer.DTO.SkillDTO;
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
    public class SkillController : ControllerBase
    {
        private readonly IAdminPanelService _adminService;
        public SkillController(IAdminPanelService adminService)
        {
            _adminService = adminService;
        }

        [Authorize]
        [HttpPost("CreateNewSkill")]
        public async Task<IActionResult> CreateNewSkill([FromForm]CreateSkillDto skillDto)
        {
            var load = _adminService.CreateNewSkill(skillDto);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpPut("UpdateSkillById")]
        public async Task<IActionResult> UpdateSkillById([FromForm] UpdateSkillDto skillDto)
        {
            var load = _adminService.UpdateSkillById(skillDto);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpDelete("DeleteSkillById")]
        public async Task<IActionResult> DeleteSkillById(int skillId)
        {
            var load = _adminService.DeleteSkillById(skillId);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("GetSkillById")]
        public async Task<IActionResult> GetSkillById(int skillId)
        {
            var load = _adminService.GetSkillById(skillId);

            if (load.StatusCode == 200)
                return Ok(load.SkillInfo);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("GetSkillList")]
        public async Task<IActionResult> GetSkillList(int currentPage, int take, bool? asc = true, string columnName = "CreateDate")
        {
            var load = _adminService.GetSkillList(currentPage, take, asc, columnName);

            if (load.StatusCode == 200)
                return Ok(new { List = load.SkillList, AllRecords = load.All });

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }
    }
}
