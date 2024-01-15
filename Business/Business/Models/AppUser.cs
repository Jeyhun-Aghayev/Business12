using Microsoft.AspNetCore.Identity;

namespace Business.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
