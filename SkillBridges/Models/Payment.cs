using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public class Payment
    {
        [Key]
        public string PaymentId { get; set; }
        [Required]
        public string TaskId { get; set; }
        public Task Task { get; set; }

        [Required]
        public string ProfessionalProfileId { get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }
        [Required]
        public string ClientProfileId { get; set; }
        public ClientProfile ClientProfile { get; set; }

        [Required]

        public decimal Amount { get; set; }


        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string PaymentStatus { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
