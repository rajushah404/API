using AppAPI.Model.RequestModel;
using AppAPI.Model.ResponseModel;
using AppAPI.Model;

namespace AppAPI.Services.Interface
{
    public interface ILoginInterface
    {
        Task<LoginResponseModel> GenerateToken(LoginRequestModel loginRequestModel);
 
    }
}
