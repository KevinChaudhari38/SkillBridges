using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class SkillViewModel
    {

        public string SkillId { get; set; }

        [Required(ErrorMessage ="Name Is required")]
        public string Name { get; set; }
        [Required(ErrorMessage="Description is Required")]
        public string Description { get; set; }

    }
}
