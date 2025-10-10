using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;
using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public class TaskRepository:ITaskRepository
    {
        private readonly SkillBridgeContext _context;
        public TaskRepository(SkillBridgeContext context)
        {
            _context = context;
        }
        
        public Models.Task GetById(string id)
        {
            return _context.Tasks.Include(t => t.ClientProfile).Include(t => t.TaskApplications).FirstOrDefault(t => t.TaskId == id);
        }
        public List<Models.Task> GetByClientId(string clientId) {
            return _context.Tasks.Include(t => t.TaskApplications).Include(c => c.ClientProfile).ThenInclude(c => c.User).Include(c => c.Category).Include(t => t.ProfessionalProfile).ThenInclude(p => p.User).Where(t => t.ClientProfileId == clientId).ToList();
        }
        public List<Models.Task> GetAll()
        {
            return _context.Tasks.Include(c=>c.ClientProfile).ThenInclude(c=>c.User).Include(c=>c.Category).Include(t => t.ProfessionalProfile).ThenInclude(p => p.User).Where(t=>t.Status==Models.TaskStatus.Open).ToList();
        }
        public List<Models.Task> GetByCategoryId(string id)
        {
            return _context.Tasks.Include(c => c.ClientProfile).ThenInclude(c => c.User).Include(c => c.Category).Include(t => t.ProfessionalProfile).ThenInclude(p => p.User).Where(t => t.CategoryId == id && t.Status==Models.TaskStatus.Open).ToList();
        }
        public List<Models.Task> GetByCategoryIdAll(string id)
        {
            return _context.Tasks.Include(c => c.ClientProfile).ThenInclude(c => c.User).Include(c => c.Category).Include(t => t.ProfessionalProfile).ThenInclude(p => p.User).Where(t => t.CategoryId == id).ToList();
        }
        public List<Models.Task> GetByProfessionalId(string professionalId)
        {
            return _context.Tasks.Include(c => c.ClientProfile).ThenInclude(c => c.User).Include(c => c.Category).Include(t => t.ProfessionalProfile).ThenInclude(p => p.User).Where(t => t.ProfessionalProfileId == professionalId).ToList();
        }
        public List<Models.Task> GetByType(TaskType type)
        {
            return _context.Tasks.Include(c => c.ClientProfile).ThenInclude(c => c.User).Include(c => c.Category).Include(t => t.ProfessionalProfile).ThenInclude(p => p.User).Where(t => t.Type == type && t.Status==Models.TaskStatus.Open).ToList();
        }
        public IEnumerable<SelectListItem> GetCities()
        {
            return Enum.GetValues(typeof(City))
                       .Cast<City>()
                       .Select(r => new SelectListItem
                       {
                           Value = ((int)r).ToString(),
                           Text = r.ToString()
                       });
        }
        public void Insert(Models.Task task)
        {
         
            task.TaskId = Guid.NewGuid().ToString();
            _context.Tasks.Add(task);
        }

        public void Update(Models.Task task)
        {
            _context.Tasks.Update(task);
        }

        public void Delete(Models.Task task)
        {
            _context.Tasks.Remove(task);
        }
    }
}
