using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SkillBridges.Models
{
    public enum UserRole
    {
        Client = 1,
        Professional = 2,
        Admin = 3
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber {  get; set; }
        public UserRole Role {  get; set; }
        public DateTime CreatedAt { get; set; }
        
        public ClientProfile ClientProfile { get; set; }
        
        public ProfessionalProfile ProfessionalProfile { get; set; }
    }
}
