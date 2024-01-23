using AppAPI.Model;
using AppAPI.Model.RequestModel;
using AppAPI.Model.ResponseModel;
using AppAPI.Services.Interface;

namespace AppAPI.Services.Implementation
{
    public class UserServicesb : DapperService,IUserServices
    {
        private IConfiguration _configuration;
        public UserServicesb(IConfiguration configuration):base(configuration) 
        {
            _configuration = configuration;   
        }
        public Task deleteuser(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> getUser()
        {
            var result = GetQueryResultAsync<UserModel>("exec spUsers @Flag=@Flag",new{
                Flag = "G",
            });

            return result;
        }

        public async Task<SaveUserResponse>saveUser(SaveUser saveUser)
        {

            var result = await GetQueryFirstOrDefaultAsync<SaveUserResponse>("exec spUsers @Flag=@Flag,@UserName=@UserName,@Password=@Password,@Name=@Name", new
            {
                Flag="S",
                Username = saveUser.Username,
                Password=saveUser.Password,
                Name=saveUser.Name,

            });
            return result;
        }
    }
}
