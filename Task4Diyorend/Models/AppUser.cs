using Microsoft.AspNetCore.Identity;

namespace Task4Diyorend.Models
{
    public class AppUser: IdentityUser
    {
        public DateTime RegistrationTime { get; set; } = DateTime.Now;
        public DateTime LastLoginTime { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public bool ActiveStatus { get; set; } = true;

    }
}
