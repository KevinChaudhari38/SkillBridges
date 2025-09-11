namespace SkillBridges.Models
{
    public class ClientProfile
    {
        public int ClientProfileId {  get; set; }
        public String OrganizationName {  get; set; }
        public string Address {  get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
      //  public ICollection<Task> Tasks { get; set; }
    }
}
