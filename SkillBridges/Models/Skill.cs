using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public class Skill
    {
        [Key]
        public string SkillId {  get; set; }
        [Required]
        public string Name {  get; set; }
        public ICollection<ProfessionalSkill> ProfessionalSkills { get; set; }
    }
}
