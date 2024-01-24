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
        public async Task<ActionResult<LoginResponseModel>> LoginResponseModel(LoginRequestModel loginRequestModel)
        {
            LoginResponseModel loginResponseModel = new LoginResponseModel();
            var Token = await _loginService.GenerateToken(loginRequestModel);
            if (Token == null)
            {
                loginResponseModel.StatusCode = "001";
                loginResponseModel.Message = "Invalid User";
                loginResponseModel.Token = null;
              
            }
            else
            {
                loginResponseModel.StatusCode = "000";
                loginResponseModel.Message = " User Verified";
                loginResponseModel.Token = Token.Token;
               
            }
            return loginResponseModel;
        }
    }
}
