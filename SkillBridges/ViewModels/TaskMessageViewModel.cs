namespace SkillBridges.ViewModels
{
    public class TaskMessageViewModel
    {
        public string MessageId {  get; set; }
        public string TaskTitle {  get; set; }
        public string SenderId {  get; set; }
        public string SenderName {  get; set; }
        public string ReceiverId {  get; set; }
        public string ReceiverName {  get; set; }
        public string Message {  get; set; }
        public DateTime SentAt {  get; set; }
        public bool IsRead {  get; set; }
    }
}
