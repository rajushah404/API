using AppAPI.Model;
using AppAPI.Model.RequestModel;
using AppAPI.Model.ResponseModel;
using AppAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UserController : Controller
    {
        private IUserServices _services;
        public UserController(IUserServices userServices)
        {
            _services = userServices;
        }
        [HttpPost("SaveUser")]
        public async Task<SaveUserResponse> SaveUser( SaveUser saveUser)
        {


          var Result =   await _services.saveUser(saveUser);
          return Result;
        }

        [HttpGet("GetUser")]
        public async Task<List<UserModel>> GetUser ()
        {
            var Result = await _services.getUser();
            return Result;
         
        }

        [HttpGet("GetUserByUsername")]
        public async Task<GetUserByNameResponse> getUserByUsername(string Username)
        {
            var Result =await _services.getUserByUsername(Username);
       
            return Result;
        }

        [HttpPut("CompleteProfile")]
        public async Task<SaveUserResponse> profileComplete(ProfileInfo profileInfo )
        {
            var Result = await _services.profileComplete(profileInfo);
            return Result;
        }
    }
}


