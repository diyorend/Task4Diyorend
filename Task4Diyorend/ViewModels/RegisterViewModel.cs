using System.ComponentModel.DataAnnotations;

namespace Task4Diyorend.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password should match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set;}
    }
}
