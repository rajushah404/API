namespace AppAPI.Model.ResponseModel
{
    public class LoginResponseModel<T>
    {
        public T TokenInfo { get; set; }

        public string StatusCode { get; set; }
        public string Message { get; set; }

    }

    public class TokenInfo
    {

        public string Token { get; set; }

        public int Status { get; set; }

        public string Username { get; set; }
    }
}


