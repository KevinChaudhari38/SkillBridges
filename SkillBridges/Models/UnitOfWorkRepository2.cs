using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class UnitOfWorkRepository2:IUnitOfWork2
    {
        private readonly SkillBridgeContext _context;
        private ITaskRepository _taskRepository;
        private ITaskApplicationRepository _taskApplicationRepository;
        private ISkillRepository _skillRepository;
        private IProfessionalSkillRepository _professionalSkillRepository;
        private ICategoryRepository _categoryRepository;

        public UnitOfWorkRepository2(SkillBridgeContext context)
        {
            _context = context;
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
                return _professionalSkillRepository ??new ProfessionalSkillRepository(_context);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
