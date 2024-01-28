using System.ComponentModel.DataAnnotations;

namespace AppAPI.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Username Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Username Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public string Password { get; set; }
        public string Address { get; set; }

        public string ContachNo { get; set; }
        public string Image { get; set; }

        public int isVerified { get; set; }
        
    }
}


