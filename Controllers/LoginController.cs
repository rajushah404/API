using AppAPI.Model.RequestModel;
using AppAPI.Model.ResponseModel;
using AppAPI.Services.Implementation;
using AppAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : Controller
    {


        private ILoginInterface _loginService;
        public LoginController(ILoginInterface loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel<TokenInfo>>> LoginResponseModel(LoginRequestModel loginRequestModel)
        {
            LoginResponseModel<TokenInfo> loginResponseModel = new LoginResponseModel<TokenInfo>();
            var Token = await _loginService.GenerateToken(loginRequestModel);
            if (Token != null && Token.StatusCode != "000")
            {
              
                return new LoginResponseModel<TokenInfo>()
                {
                    TokenInfo = Token.TokenInfo,
                    StatusCode = Token.StatusCode,
                    Message = Token.Message,
                };
            }
            else
            {

                return new LoginResponseModel<TokenInfo>()
                {
                    TokenInfo = Token.TokenInfo,
                    StatusCode = Token.StatusCode,
                    Message = Token.Message,
                };

            }
        }
    }
}
