
namespace SkillBridges.ViewModels
{
    public class TaskViewModel
    {
        public string TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budjet { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
        public string? RazorpayOrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? Deadline { get; set; }
        public string ClientProfileId { get; set; }
        public string ClientName { get; set; }
        public string? ProfessionalProfileId { get; set; }
        public string? ProfessionalName { get; set; }
        public string Status { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
