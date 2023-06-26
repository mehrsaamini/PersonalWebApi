using PersonalWeb.BusinessLayer.DTO.ServiceDTO;
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
    public class ServiceController : ControllerBase
    {
        private readonly IAdminPanelService _adminService;
        public ServiceController(IAdminPanelService adminService)
        {
            _adminService = adminService;
        }

        [Authorize]
        [HttpPost("CreateNewService")]
        public async Task<IActionResult> CreateNewService([FromForm] CreateService serviceDto)
        {
            var load = _adminService.CreateNewService(serviceDto);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpPut("UpdateServiceById")]
        public async Task<IActionResult> UpdateServiceById([FromForm] UpdateServiceDto serviceDto)
        {
            var load = _adminService.UpdateServiceById(serviceDto);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [Authorize]
        [HttpDelete("DeleteServiceById")]
        public async Task<IActionResult> DeleteServiceById(int serviceId)
        {
            var load = _adminService.DeleteServiceById(serviceId);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("GetServiceById")]
        public async Task<IActionResult> GetServiceById(int serviceId)
        {
            var load = _adminService.GetServiceById(serviceId);

            if (load.StatusCode == 200)
                return Ok(load.ServiceInfo);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("GetServiceList")]
        public async Task<IActionResult> GetServiceList(int currentPage, int take, bool? asc = true, string columnName = "CreateDate")
        {
            var load = _adminService.GetServiceList(currentPage, take, asc, columnName);

            if (load.StatusCode == 200)
                return Ok(new { List = load.ServiceList, AllRecords = load.All });

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }
    }
}
