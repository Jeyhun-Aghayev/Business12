using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels.Account
{
    public class LoginVm
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }    
    }
}
