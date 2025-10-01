using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly SkillBridgeContext _context;

        private IUserRepository _userRepository;
        private IClientRepository _clientRepository;
        private IProfessionalRepository _professionalRepository;
        private ITaskRepository _taskRepository;
        private ITaskApplicationRepository _taskApplicationRepository;
        private ISkillRepository _skillRepository;
        private IProfessionalSkillRepository _professionalSkillRepository;
        private ICategoryRepository _categoryRepository;
        private IMessageRepository _messageRepository;
        private IWorkRepository _workRepository;
        private IRatingRepository _ratingRepository;
        private IPaymentRepository _payments;
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
        public ITaskRepository Tasks
        {
            get
            {
                return _taskRepository ??= new TaskRepository(_context);
            }
        }
        public ITaskApplicationRepository TaskApplications
        {
            get
            {
                return _taskApplicationRepository ??= new TaskApplicationRepository(_context);
            }
        }
        public ICategoryRepository Categories
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(_context);
            }
        }
        public ISkillRepository Skills
        {
            get
            {
                return _skillRepository ??= new SkillRepository(_context);
            }
        }
        public IProfessionalSkillRepository ProfessionalSkills
        {
            get
            {
                return _professionalSkillRepository ??= new ProfessionalSkillRepository(_context);
            }
        }
        public IMessageRepository Messages
        {
            get
            {
                return _messageRepository ??= new MessageRepository(_context);
            }
        }
        public IWorkRepository WorkSubmissions
        {
            get
            {
                return _workRepository??=new WorkRepository(_context);
            }
        }
        public IRatingRepository Ratings{
            get
            {
                return _ratingRepository??= new RatingRepository(_context);
            }
        }

        public IPaymentRepository Payments => _payments ??= new PaymentRepository(_context);
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
