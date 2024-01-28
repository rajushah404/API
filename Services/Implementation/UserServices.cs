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
            var result = GetQueryResultAsync<UserModel>("exec spUserInfo @Flag=@Flag", new{
                Flag = "G",
            });

            return result;
        }

        public async Task<SaveUserResponse>saveUser(SaveUser saveUser)
        {

            var result = await GetQueryFirstOrDefaultAsync<SaveUserResponse>("exec spUserInfo @Flag=@Flag,@UserName=@UserName,@Password=@Password,@Name=@Name,@isVerified = @isVerified", new
            {
                Flag="S",
                Username = saveUser.Username,
                Password=saveUser.Password,
                Name=saveUser.Name,
                isVerified = 0
                


            });
            return result;
        }

        public async Task<SaveUserResponse> profileComplete(ProfileInfo model)
        {

            var result = await GetQueryFirstOrDefaultAsync<SaveUserResponse>("exec spUserInfo @Flag=@Flag,@Address=@Address,@Email=@Email,@ContachNo=@ContachNo,@Image=@Image,@UserName = @UserName,@isVerified = @isVerified", new
            {
                Flag = "U",
                UserName = model.Username,
                Address = model.Address,
                Email = model.Email,
                ContachNo = model.ContachNo,
                Image = model.Image,
                isVerified = 1
                

            });
            return result;
        }



        public async Task<GetUserByNameResponse> getUserByUsername(string Username)
        {
            GetUserByNameResponse getUserByNameResponse = new GetUserByNameResponse();
            var getUser = await GetQueryFirstOrDefaultAsync<UserModel>("exec spUserInfo @Flag=@Flag, @UserName = @UserName", new
            {
                Flag = "N",
                UserName = Username
                
            });

            if(getUser == null)
            {
                getUserByNameResponse.data = null;
                getUserByNameResponse.Message = "User doesnot exits.";
                   

            }else
            {
                getUserByNameResponse.data = getUser;
                getUserByNameResponse.Message = "User Found";
            }

            return getUserByNameResponse;

        }
    }
}
