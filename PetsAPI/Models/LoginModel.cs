using System.ComponentModel.DataAnnotations;

namespace PetsAPI.Models
{
    public class Login
    {
        [Required]
        public string UserNameEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
