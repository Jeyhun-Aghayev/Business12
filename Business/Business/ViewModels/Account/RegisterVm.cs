using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels.Account
{
    public class RegisterVm
    {
        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string Surname { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(40)]
        public string UserName { get; set; }
        public string Email { get; set; }
        [MinLength(8)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
