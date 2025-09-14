namespace SkillBridges.Models
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IClientRepository Clients { get; }
        IProfessionalRepository Professionals { get; }
        void Save();
    }
}
