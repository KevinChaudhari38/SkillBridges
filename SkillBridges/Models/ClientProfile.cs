using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public class ClientProfile
    {
        [Key]
        public string ClientProfileId { get; set; }

        [Required(ErrorMessage = "Organization Name is required")]
        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Task> Tasks { get; set; }
        public ICollection<TaskApplication> TaskApplications { get; set; }
    }
}
