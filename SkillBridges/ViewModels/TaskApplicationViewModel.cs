namespace SkillBridges.ViewModels
{
    public class TaskApplicationViewModel
    {
        public string TaskApplicationId {  get; set; }
        public string TaskId {  get; set; }
        public string TaskTitle {  get; set; }
        public string ClientProfileId {  get; set; }
        public string ClientName {  get; set; }
        public string ProfessionalProfileId {  get; set; }
        public string ProfessionalName { get;set; }
        public string Proposal {  get; set; }
        public List<String> Skills { get; set; }
        public decimal ExpectedBudjet {  get; set; }
        public DateTime AppliedAt {  get; set; }
        public string Status {  get; set; }
        public string File {  get; set; }
    }
}
