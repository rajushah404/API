using AppAPI.Model;
using AppAPI.Model.RequestModel;
using AppAPI.Model.ResponseModel;

namespace AppAPI.Services.Interface
{
    public interface IUserServices
    {
        Task<SaveUserResponse> saveUser(SaveUser saveUser);
        Task<SaveUserResponse> profileComplete( ProfileInfo profileInfo);
        Task deleteuser(int Id);
        Task<List<UserModel>> getUser();
        Task<GetUserByNameResponse> getUserByUsername(string Username ); 
    }
}
