using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public class Rating
    {
        [Key]
        public string RatingId { get; set; }
        [Required]
        public string TaskId { get; set; }
        public Task Task { get; set; }

        [Required]
        public string ProfessionalProfileId { get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }
        [Required]
        public string ClientProfileId { get; set; }
        public ClientProfile ClientProfile { get; set; }

        [Required]
        [Range(1,5,ErrorMessage ="Rating Must be between 1 to 5")]
        public int Score { get; set; }

        [MaxLength(100)]
        public string Feedback { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
