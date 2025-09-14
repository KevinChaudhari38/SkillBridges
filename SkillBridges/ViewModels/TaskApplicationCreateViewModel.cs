using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class TaskApplicationCreateViewModel
    {
        [Required]
        public string TaskId {  get; set; }
        [Required]
        public string ClientProfileId {  get; set; }
        [Required]
        public string ProfessionalProfileId {  get; set; }

        [Required(ErrorMessage ="Proposal Is Required")]
        public string Proposal {  get; set; }
        [Required(ErrorMessage = "Expected budget is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Expected budget should be positive")]
        public decimal ExpectedBudjet { get; set; }
    }
}
