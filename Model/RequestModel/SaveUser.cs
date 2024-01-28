using System.ComponentModel.DataAnnotations;

namespace AppAPI.Model.RequestModel
{
    public class SaveUser
    {
        [Required(ErrorMessage = " Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Username Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }


    }
}
