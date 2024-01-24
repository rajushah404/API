using AppAPI.Model;
using AppAPI.Model.RequestModel;
using AppAPI.Model.ResponseModel;
using AppAPI.Services.Interface;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AppAPI.Services.Implementation
{
    public class LoginService : DapperService, ILoginInterface
    {
        private IConfiguration _configuration;
        public LoginService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<LoginResponseModel> GenerateToken(LoginRequestModel loginRequestModel)
        {
            LoginResponseModel responseModel = new LoginResponseModel();

            var userDetails = await GetQueryFirstOrDefaultAsync<UserModel>(
                "exec spLogin @Flag=@Flag,@UserName=@UserName", new
                {
                    Flag = "L",
                    UserName = loginRequestModel.Username
                });
            if (loginRequestModel.Password != userDetails.Password)
            {
                return null;
            }
            else
            {
                var authClaims = new List<Claim>
            {
                new Claim("UserId",userDetails.Id.ToString()),
                new Claim("Username",userDetails.Username.ToString())
            };
                var token = GenerateToken(authClaims);
                responseModel.Token = token.Token;
                return responseModel;
            }


        }

        private LoginResponseModel GenerateToken(List<Claim> claims)
        {
            LoginResponseModel model = new LoginResponseModel();    
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));
            _ = int.TryParse(_configuration["AppSettings:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            model.Token = tokenString;

            return model;
        }
    }
}
