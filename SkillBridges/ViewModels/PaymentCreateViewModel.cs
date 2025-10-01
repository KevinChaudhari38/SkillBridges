namespace SkillBridges.ViewModels
{
    public class PaymentCreateViewModel
    {
        public string TaskId { get; set; }
        public decimal Amount { get; set; }
        public string RazorpayKey { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpayOrderId { get; set; }
        public string RazorpaySignature { get; set; }

    }
}
