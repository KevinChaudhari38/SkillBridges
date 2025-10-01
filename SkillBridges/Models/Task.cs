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
        Done,
        Rejected,
        Cancelled
    }
    public enum TaskType
    {
        Local,
        Global
    }
    public enum City
    {
        Mumbai,
        Ahmedabad,
        Nadiad,
        Vadodara,
        Surat,
        Rajkot
    }
    public class Task
    {
        [Key]
        public string TaskId {  get; set; }

        [Required(ErrorMessage="Title is required")]
        public string Title { get; set;}

        [Required(ErrorMessage="Description Is Required")]
        public string Description { get; set; }

        public decimal Budjet {  get; set; }
        public string PaymentStatus { get; set; } = "Pending";
        public string? RazorpayOrderId { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime? Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public TaskType Type { get; set; }
        public City? City { get; set; }
        [Required]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public string ClientProfileId {  get; set; }
        public ClientProfile ClientProfile {  get; set; }
        public string? ProfessionalProfileId {  get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }

        public ICollection<TaskApplication> TaskApplications { get; set; }
        public List<TaskMessage>? Messages { get; set; }

       

    }
}
