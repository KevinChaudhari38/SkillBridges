namespace SkillBridges.ViewModels
{
    public class ClientDetailsViewModel
    {
        public string ClientProfileId {  get; set; }
        public string OrganizationName {  get; set; }
        public string Address {  get; set; }
        public UserViewModel User { get; set; }
    }
}
