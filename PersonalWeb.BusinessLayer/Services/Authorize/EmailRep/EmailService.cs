using PersonalWeb.BusinessLayer.DTO.Email;
using PersonalWeb.BusinessLayer.HelperClass;
using PersonalWeb.BusinessLayer.Services.Authorize.Token;
using PersonalWeb.DataLayer.Context;
using PersonalWeb.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.Services.Authorize.EmailRep
{
    public class EmailService : IEmailService
    {
        private readonly AuthorizeContext _authorizeContext;
        private readonly ITokenService _tokenService;
        private readonly IViewRenderService _viewRender;

        public EmailService(AuthorizeContext authorizeContext, ITokenService tokenService
                            , IViewRenderService viewRender)
        {
            _authorizeContext = authorizeContext;
            _tokenService = tokenService;
            _viewRender = viewRender;
        }

        public (int StatusCode, string Message) Register(EmailCodeDto emailCode)
        {
            try
            {
                var random = new Random();
                var emailNumberCode = random.Next(194751, 854198).ToString();

                var user = _authorizeContext.Users.FirstOrDefault(u => u.Email == emailCode.EmailAddress);
                if (user != null)
                    return (400, "u can't use this email. another player use this email");

                #region Add User
                var newUser = new User()
                {
                    MobileNumber = "default",
                    EmailCode = emailNumberCode,
                    AccessAdminPanel = true,
                    Name = "Admin",
                    Family = "Admin",
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(15),
                    RefreshToken = "default",

                    Email = emailCode.EmailAddress,

                    CreateDate = DateTime.Now,
                    ProjectDescription = "Admin",
                };
                _authorizeContext.Users.Add(newUser);
                _authorizeContext.SaveChanges();
                #endregion

                return (200, "Success to Register");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message) SendEmail(string email)
        {
            try
            {
                var existUser = _authorizeContext.Users.SingleOrDefault(u => u.Email == email);

                if (existUser == null)
                    return (404, "Your email not exist in system. You don't access adminpanel.");

                var random = new Random();
                var emailNumberCode = random.Next(194751, 854198).ToString();
                
                existUser.EmailCode = emailNumberCode;
                _authorizeContext.Users.Update(existUser);
                _authorizeContext.SaveChanges();

                #region SendEmail
                string body = _viewRender.RenderToStringAsync("EmailCode", existUser);
                SendingEmail.SendEmail(existUser.Email, "Login", body);
                #endregion

                return (200, "Success to Send Email");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message, string newToken, string newRefreshToken) GetRefreshToken(TokenDto tokenApiModel)
        {
            try
            {
                if (tokenApiModel is null)
                    return (400, "Invalid client request", string.Empty, string.Empty);

                var principal = _tokenService.GetPrincipalFromExpiredToken(tokenApiModel.AccessToken);
                var userId = principal.Identity.Name;

                var user = _authorizeContext.Users.SingleOrDefault(u => u.UserId == int.Parse(userId));

                if (user == null || user.RefreshToken != tokenApiModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                    return (400, "Invalid client request", string.Empty, string.Empty);

                var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
                user.RefreshToken = _tokenService.GenerateRefreshToken();

                _authorizeContext.Users.Update(user);
                _authorizeContext.SaveChanges();

                return (200, "Success", newAccessToken, user.RefreshToken);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, string.Empty, string.Empty);
            }
        }

        public (int StatusCode, string Message) Revoke(int userId)
        {
            try
            {
                var user = _authorizeContext.Users.SingleOrDefault(u => u.UserId == userId);
                if (user == null)
                    return (404, "Not found the User");

                //user.RefreshToken = null;
                user.RefreshToken = string.Empty;

                _authorizeContext.Users.Update(user);
                _authorizeContext.SaveChanges();

                return (200, "Success to logout.");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message, string AccessToken, string RefreshToken) Login(VerifyEmailDto emailDto)
        {
            try
            {
                var existUser = _authorizeContext.Users.SingleOrDefault(u => u.Email == emailDto.Email);

                if (existUser == null)
                    return (404, "You don't access admin panel", string.Empty, string.Empty);

                if (existUser.EmailCode != emailDto.EmailCode)
                    return (400, "The EmailCode is incorrect.", string.Empty, string.Empty);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existUser.UserId.ToString()),
                };

                existUser.RefreshToken = _tokenService.GenerateRefreshToken();
                existUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                _authorizeContext.Users.Update(existUser);
                _authorizeContext.SaveChanges();

                var accessToken = _tokenService.GenerateAccessToken(claims);
                return (200, "Success to login", accessToken, existUser.RefreshToken);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, string.Empty, string.Empty);
            }
        }
    }
}
