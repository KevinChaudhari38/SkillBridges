using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;

namespace SkillBridges.ViewModels
{
    public enum UserRole
    {
        Client = 1,
        Professional = 2,
       
    }
    public class UserCreateViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        
        public IEnumerable<SelectListItem>? Roles { get; set; } = new List<SelectListItem>();
    }
}
