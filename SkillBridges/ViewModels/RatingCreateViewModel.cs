using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class RatingCreateViewModel
    {
        [Required]
        public string TaskId { get; set; }

        [Required]
        public string ProfessionalProfileId { get; set; }
        [Required]
        public string ClientProfileId {  get; set; }

        [Required]
        [Range(1, 5)]
        public int Score { get; set; }

        [MaxLength(100)]
        public string Feedback { get; set; }
    }
}
