using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public enum ApplicationStatus
    {
        Pending,
        Accepted,
        Rejected
    }
    public class TaskApplication
    {
        [Key]
        public string TaskApplicationId {  get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.Now;
        public string Proposal {  get; set; }
        public decimal ExpectedBudjet {  get; set; }
        [Required]
        public ApplicationStatus Status {  get; set; }=ApplicationStatus.Pending;
        public string ClientProfileId {  get; set; }
        public ClientProfile ClientProfile {  get; set; }
        [Required]
        public string TaskId {  get; set; }
        public Task Task { get; set; }
        [Required]
        public string ProfessionalProfileId {  get; set; }
        public ProfessionalProfile ProfessionalProfile {  get; set; }
    }
}
