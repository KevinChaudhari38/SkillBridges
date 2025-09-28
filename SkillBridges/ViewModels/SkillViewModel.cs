using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class SkillViewModel
    {

        public string SkillId { get; set; }

        
        public string Name { get; set; }
 
        public string Description { get; set; }
        public string? ProfessionalProfileId { get; set; }
    }
}
