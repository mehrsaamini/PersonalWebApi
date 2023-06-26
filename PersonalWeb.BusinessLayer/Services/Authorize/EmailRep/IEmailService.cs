using PersonalWeb.BusinessLayer.DTO.Email;
using PersonalWeb.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.Services.Authorize.EmailRep
{
    public interface IEmailService
    {
        (int StatusCode, string Message) Register(EmailCodeDto emailCode);
        (int StatusCode, string Message) SendEmail(string email);
        (int StatusCode, string Message, string AccessToken, string RefreshToken) Login(VerifyEmailDto vr);
        (int StatusCode, string Message, string newToken, string newRefreshToken) GetRefreshToken(TokenDto tokenApiModel);
        (int StatusCode, string Message) Revoke(int userId);
    }
}
