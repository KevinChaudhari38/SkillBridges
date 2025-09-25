using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class TaskRepository:ITaskRepository
    {
        private readonly SkillBridgeContext _context;
        public TaskRepository(SkillBridgeContext context)
        {
            _context = context;
        }
        public Task GetById(string id)
        {
            return _context.Tasks.Include(t => t.ClientProfile).Include(t => t.TaskApplications).FirstOrDefault(t => t.TaskId == id);
        }
        public List<Task> GetByClientId(string clientId) {
            return _context.Tasks.Include(t => t.TaskApplications).Where(t => t.ClientProfileId == clientId).ToList();
        }
        public List<Task> GetAll()
        {
            return _context.Tasks.ToList();
        }
        public List<Task> GetByCategoryId(string id)
        {
            return _context.Tasks.Where(t => t.CategoryId == id).ToList();
        }
        public void Insert(Task task)
        {
         
            task.TaskId = Guid.NewGuid().ToString();
            _context.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            _context.Tasks.Update(task);
        }

        public void Delete(Task task)
        {
            _context.Tasks.Remove(task);
        }
    }
}
