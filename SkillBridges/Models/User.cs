using Microsoft.EntityFrameworkCore;
using SkillBridges.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public enum UserRole
    {
        Client = 1,
        Professional = 2,
        Admin = 3
    }
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character")]
        public string Password { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Required(ErrorMessage ="Phone number is required")]
        public string PhoneNumber { get; set; }

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }
        public ClientProfile? ClientProfile { get; set; }
        public ProfessionalProfile? ProfessionalProfile { get; set; }


      
    }
}
