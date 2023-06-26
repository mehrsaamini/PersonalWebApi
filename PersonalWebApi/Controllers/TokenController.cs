using PersonalWeb.BusinessLayer.DTO.Email;
using PersonalWeb.BusinessLayer.Services.Authorize.EmailRep;
using PersonalWeb.BusinessLayer.Services.Authorize.Token;
using PersonalWeb.DataLayer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class TokenController : ControllerBase
    {
        private readonly AuthorizeContext _AuthorizeContext;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public TokenController(AuthorizeContext AuthorizeContext, ITokenService tokenService,
                                IEmailService emailService)
        {
            _emailService = emailService;
            _AuthorizeContext = AuthorizeContext;
            _tokenService = tokenService;
        }

        [HttpPut("refresh")]
        public IActionResult Refresh(TokenDto tokenApiModel)
        {
            var load = _emailService.GetRefreshToken(tokenApiModel);
            if (load.StatusCode == 200)
                return Ok(new { load.newToken, load.newRefreshToken });

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("revoke")]
        [Authorize]
        public IActionResult Revoke()
        {
            var userId = int.Parse(User.Identity.Name);

            var load = _emailService.Revoke(userId);

            if (load.StatusCode == 200)
                return NoContent();

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        [HttpGet("SendEmail")]
        public async Task<IActionResult> SendEmail(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(email);

            var load = _emailService.SendEmail(email);

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }

        //[HttpPost("Register")]
        //public async Task<IActionResult> Register(EmailCodeDto emailCode)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(emailCode);

        //    var load = _emailService.Register(emailCode);

        //    return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        //}

        [HttpPost("Login")]
        public async Task<IActionResult> Login(VerifyEmailDto emailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(emailDto);

            var load = _emailService.Login(emailDto);

            if (load.StatusCode == 200)
                return Ok(new { AccessToken = load.AccessToken, RefreshToken = load.RefreshToken });

            return StatusCode(load.StatusCode, new { load.StatusCode, load.Message });
        }
    }
}
