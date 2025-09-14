using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly SkillBridgeContext _context;

        private IUserRepository _userRepository;
        private IClientRepository _clientRepository;
        private IProfessionalRepository _professionalRepository;

        public UnitOfWorkRepository(SkillBridgeContext context)
        {
            _context = context;
        }

        public IUserRepository Users
        {
            get { return _userRepository ??= new UserRepository(_context); }
        }

        public IClientRepository Clients
        {
            get { return _clientRepository ??= new ClientRepository(_context); }
        }

        public IProfessionalRepository Professionals
        {
            get { return _professionalRepository ??= new ProfessionalRepository(_context); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
