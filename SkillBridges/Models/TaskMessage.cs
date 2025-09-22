using System.ComponentModel.DataAnnotations;

namespace SkillBridges.Models
{
    public class TaskMessage
    {
        [Key]
        public string MessageId {  get; set; }
        [Required]
        public string TaskId {  get; set; }
        public Task Task { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public UserRole SenderRole {  get; set; }
        [Required]
        public string ReceiverId { get; set; }  
        [Required]
        public string Message {  get; set; }

        public DateTime SentAt { get; set; }=DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}
