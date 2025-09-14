
namespace SkillBridges.ViewModels
{
    public class TaskViewModel
    {
        public string TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budjet { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public string ClientProfileId { get; set; }
        public string? ProfessionalProfileId { get; set; }
        public TaskStatus Status { get; set; }
        public string CategoryId { get; set; }
    }
}
