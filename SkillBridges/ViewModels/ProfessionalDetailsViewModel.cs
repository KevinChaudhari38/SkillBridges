namespace SkillBridges.ViewModels
{
    public class ProfessionalDetailsViewModel
    {
        public string ProfessionalProfileId {  get; set; }
        public string Bio {  get; set; }
        public string Location {  get; set; }
        public string Languages {  get; set; }
        public bool IsAvailable {  get; set; }
        public UserViewModel User { get; set; }
    }
}
