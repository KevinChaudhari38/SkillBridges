using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {  get; set; }
        [Required]
        public string Name {  get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}