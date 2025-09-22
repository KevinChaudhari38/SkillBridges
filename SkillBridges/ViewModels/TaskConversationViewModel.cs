namespace SkillBridges.ViewModels
{
    public class TaskConversationViewModel
    {
        public string TaskId {  get; set; }
        public string ClientProfileId {  get; set; }
        public string ProfessionalProfileId {  get; set; }
        public string Role {  get; set; }
        public List<TaskMessageViewModel> Messages { get; set; } 
    }
}
