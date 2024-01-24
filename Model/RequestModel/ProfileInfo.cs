using System.ComponentModel.DataAnnotations;

namespace AppAPI.Model.RequestModel
{
    public class ProfileInfo
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
 
        public string? Address { get; set; }

        public string? ContachNo { get; set; }
        public string? Image { get; set; }
      

    }
}
