

using SkillBridges.Models;

namespace SkillBridges.ViewModels
{
    public class ProfessionalTasksViewModel
    {
        public string ProfessionalProfileId { get; set; }
        public List<TaskViewModel> Tasks { get; set; }
        public List<Category> Categories {  get; set; }
        public string SelectedCategoryId {  get; set; }
        public List<TaskApplication> TaskApplications { get; set; }
    }
}
