using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SkillBridges.Models
{
    public enum TaskStatus
    {
        Open,
        Assigned,
        InProgress,
        Submitted,
        Completed,
        Rejected,
        Cancelled
    }
    public class Task
    {
        [Key]
        public string TaskId {  get; set; }

        [Required(ErrorMessage="Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage="Description Is Required")]
        public string Description { get; set; }

        public decimal Budjet {  get; set; }
        public DateTime CreateAt { get; set; }= DateTime.Now;
        public DateTime? Deadline { get; set; }
        public TaskStatus Status { get; set; }

        [Required]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public string ClientProfileId {  get; set; }
        public ClientProfile ClientProfile {  get; set; }
        public string? ProfessionalProfileId {  get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }

        public ICollection<TaskApplication> TaskApplications { get; set; }

    }
}
