using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public class Class
    {
        [Key]
        public string ReviewId {  get; set; }
        [Required]
        public string ReviewerId {  get; set; }
        public string ReviewerUserId {  get; set; }
        [Range(1,5,ErrorMessage ="Rating must be between 1 to 5")]
        public int Rating {  get; set; }
        public string Comment {  get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
