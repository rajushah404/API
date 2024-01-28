using AppAPI.Model.RequestModel;
using AppAPI.Model.ResponseModel;
using AppAPI.Model;

namespace AppAPI.Services.Interface
{
    public interface ILoginInterface
    {
        Task<LoginResponseModel<TokenInfo>> GenerateToken(LoginRequestModel loginRequestModel);
 
    }
}
