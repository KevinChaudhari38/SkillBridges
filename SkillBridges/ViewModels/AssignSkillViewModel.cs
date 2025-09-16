namespace SkillBridges.ViewModels
{
    public class AssignSkillViewModel
    {
        public string ProfessionalProfileId {  get; set; }
        public List<string> SelectedSkillIds { get; set; } = new List<string>();
        public List<SkillViewModel>? AvailableSkills { get; set; } = new List<SkillViewModel>();
    }
}
