using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character")]
        public string Password { get; set; }


        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid mobile number")]
        public string PhoneNumber { get; set; }
        
        
        
    }
}
