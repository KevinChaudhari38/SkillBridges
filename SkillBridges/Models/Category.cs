using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; }
        [Required]
        public string Name {  get; set; }
        public List<Task>? Tasks { get; set; }
    }
}