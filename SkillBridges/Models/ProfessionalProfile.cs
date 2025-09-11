namespace SkillBridges.Models
{
    public class ProfessionalProfile
    {
        public int ProfessionalProfileId { get; set; }
        public string Bio {  get; set; }
        public string Location {  get; set; }
        public string Languages {  get; set; }
        public bool IsAvailable {  get; set; }
        public int UserId {  get; set; }
        public User User { get; set; }
        //public ICollection<ProfessionalSkill> Skills { get; set; }
        //public ICollection<TaskApplication> TaskApplications { get; set; }
       // public ICollection<Task> Tasks { get; set; }
    }
}
