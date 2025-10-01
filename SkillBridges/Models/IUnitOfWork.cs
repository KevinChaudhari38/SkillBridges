namespace SkillBridges.Models
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IClientRepository Clients { get; }
        IProfessionalRepository Professionals { get; }
        ITaskRepository Tasks { get; }
        ITaskApplicationRepository TaskApplications { get; }
        ISkillRepository Skills { get; }
        IProfessionalSkillRepository ProfessionalSkills { get; }
        ICategoryRepository Categories { get; }
        IMessageRepository Messages { get; }
        IWorkRepository WorkSubmissions { get; }
        IRatingRepository Ratings { get; }
        IPaymentRepository Payments { get; }
        void Save();
    }
}
