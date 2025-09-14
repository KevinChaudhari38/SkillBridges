namespace SkillBridges.Models
{
    public interface IUnitOfWork2
    {
        ITaskRepository Tasks {  get; }
        ITaskApplicationRepository TaskApplications { get; }
        ISkillRepository Skills {  get; }
        IProfessionalSkillRepository ProfessionalSkills { get; }
        ICategoryRepository Categories { get; }
        void Save();
    }
}
