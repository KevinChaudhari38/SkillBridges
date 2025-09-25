using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class SkillCreateViewModel
    {
        [Required(ErrorMessage = "Name Is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
    }
}
