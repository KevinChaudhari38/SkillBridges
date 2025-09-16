using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public class ProfessionalProfile
    {
        [Key]
        public string ProfessionalProfileId { get; set; }

        [Required(ErrorMessage = "Bio is required")]
        public string Bio { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Languages are required")]
        public string Languages { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
        public ICollection<ProfessionalSkill> Skills { get; set; } = new List<ProfessionalSkill>();
        public ICollection<Task> Tasks { get; set; }
        public ICollection<TaskApplication> TaskApplications { get; set; }
        
    }
}
