using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class ProfessionalEditViewModel
    {
        public string ProfessionalProfileId { get; set; }
        [Required]
        public string Bio { get; set; }

        [Required]
        public string Location { get; set; }

        public string Languages { get; set; }

        public bool IsAvailable { get; set; }
    }
}
