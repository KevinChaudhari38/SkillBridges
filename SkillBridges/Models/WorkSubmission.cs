using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public enum WorkStatus
    {
        UnderSupervision,
        Accepted,
        Rejected
    }
    public class WorkSubmission
    {
        [Key]
        public string WorkSubmissionId {  get; set; }
        [Required]
        public string TaskId {  get; set; }
        public Task Task { get; set; }
        [Required]
        public string ProfessionalProfileId {  get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }
        [Required]
        public string Notes {  get; set; }
        [Required]
        public string? FilePath {  get; set; }
        public DateTime SubmittedAt { get; set; }=DateTime.Now;
        public WorkStatus Status { get; set; }=WorkStatus.UnderSupervision;
    }
}
