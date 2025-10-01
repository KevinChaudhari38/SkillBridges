using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class PaymentViewModel
    {
        [Key]
        public string PaymentId { get; set; }
        public string TaskName { get; set; }
        public string ClientName {  get; set; }
        public string ProfessionalName { get; set; }

        [Required]
        public decimal Amount { get; set; }


        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string PaymentStatus { get; set; }

        public DateTime CreatedAt { get; set; } 
    }
}
