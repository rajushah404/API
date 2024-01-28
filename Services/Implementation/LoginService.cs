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

        public async Task<LoginResponseModel<TokenInfo>> GenerateToken(LoginRequestModel loginRequestModel)
        {
            LoginResponseModel<TokenInfo> responseModel = new LoginResponseModel<TokenInfo>();
            TokenInfo tokenInfo = new TokenInfo();  

            var userDetails = await GetQueryFirstOrDefaultAsync<UserModel>(
                "exec spLogin @Flag=@Flag,@UserName=@UserName", new
                {
                    Flag = "L",
                    UserName = loginRequestModel.Username
                });
            if (userDetails == null) {
                return new LoginResponseModel<TokenInfo>()
                {
                    StatusCode = "003",
                    Message = "  User not found .",
                    TokenInfo = null,
                };

            }
            if (loginRequestModel.Password != userDetails.Password)
            {
                return new LoginResponseModel<TokenInfo>()
                {
                    StatusCode = "002",
                    Message = "Incorrect Username or password.",
                    TokenInfo = null,
                };
            }
            else
            {
                var authClaims = new List<Claim>
            {
                new Claim("UserId",userDetails.Id.ToString()),
                new Claim("Username",userDetails.Username.ToString())
            };
                var token = GenerateToken(authClaims);
                if(token.TokenInfo.Token != null)
                {
                    tokenInfo.Token = token.TokenInfo.Token;
                    tokenInfo.Username = userDetails.Username;
                    tokenInfo.Status = userDetails.isVerified;
                    return new LoginResponseModel<TokenInfo>()
                    {
                        StatusCode="000",
                        Message= "User Verified",
                        TokenInfo = tokenInfo,
                    };
                }
                else
                {
                    return new LoginResponseModel<TokenInfo>()
                    {
                        StatusCode = "001",
                        Message = "Invalid User",
                        TokenInfo = null,
                    };
                }
                
            }


        }

        private LoginResponseModel<TokenInfo> GenerateToken(List<Claim> claims)
        {
            LoginResponseModel<TokenInfo> model = new LoginResponseModel<TokenInfo>();
            TokenInfo tokenInfo = new TokenInfo();  
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));
            _ = int.TryParse(_configuration["AppSettings:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            tokenInfo.Token = tokenString;
            
            return new LoginResponseModel<TokenInfo>()
            {
                TokenInfo = tokenInfo
            };
        }
    }
}
