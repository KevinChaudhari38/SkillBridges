using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        public List<Task>? Tasks { get; set; }
    }
}
