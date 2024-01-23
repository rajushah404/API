using System.ComponentModel.DataAnnotations;

namespace AppAPI.Model
{
    public class UserModel
    {
        [Required (ErrorMessage = "User Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Username Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Username Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Email Required")]
     
        public string Address { get; set; }
 
        public string ContactNo { get; set; }
        public String Photo {  get; set; }
    }
}
