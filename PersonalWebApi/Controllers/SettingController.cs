using PersonalWeb.BusinessLayer.DTO.SettingDTO;
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
    public class SettingController : ControllerBase
    {
        private readonly IAdminPanelService _adminService;
        public SettingController(IAdminPanelService adminService)
        {
            _adminService = adminService;
        }

        [Authorize]
        [HttpPost("CreateNewSetting")]
        public async Task<IActionResult> CreateNewSetting([FromForm] CreateGeneralSettingDto infoDto)
        {
            var load =  _adminService.CreateNewSetting(infoDto);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpPut("UpdateSettingById")]
        public async Task<IActionResult> UpdateSettingById([FromForm] UpdateGeneralSettingDto infoDto)
        {
            var load =  _adminService.UpdateSettingById(infoDto);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpDelete("DeleteSettingById")]
        public async Task<IActionResult> DeleteSettingById(int settingId)
        {
            var load =  _adminService.DeleteSettingById(settingId);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("GetSettingById")]
        public async Task<IActionResult> GetSettingById(int settingId)
        {
            var load =  _adminService.GetSettingById(settingId);

            if (load.StatusCode == 200)
                return Ok(load.GeneralSettingInfo);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("GetSettingByType")]
        public async Task<IActionResult> GetSettingByType(string type)
        {
            var load = _adminService.GetSettingByType(type);

            if (load.StatusCode == 200)
                return Ok(load.GeneralSettingInfo);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpGet("GetSettingsList")]
        public async Task<IActionResult> GetSettingsList(int currentPage, int take, bool? asc = true, string columnName = "CreateDate")
        {
            var load =  _adminService.GetSettingsList(currentPage, take, asc, columnName);

            if (load.StatusCode == 200)
                return Ok(new { List = load.GeneralSettingInfo, AllRecords = load.All });

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }
    }
}
