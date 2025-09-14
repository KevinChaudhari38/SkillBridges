namespace SkillBridges.Models
{
    public class ProfessionalSkill
    {
        public string ProfessionalProfileId {  get; set; }
        public ProfessionalProfile ProfessionalProfile {  get; set; }
        public string SkillId {  get; set; }
        public Skill Skill { get; set; }
    }
}
