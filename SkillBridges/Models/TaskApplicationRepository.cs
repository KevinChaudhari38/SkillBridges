using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class TaskApplicationRepository:ITaskApplicationRepository
    {
        private readonly SkillBridgeContext _context;
        public TaskApplicationRepository(SkillBridgeContext context) { 
            _context = context;
        }
        public TaskApplication GetById(string id) {
            return _context.TaskApplications.Include(ta => ta.Task).Include(ta => ta.ProfessionalProfile).ThenInclude(p=>p.Skills).ThenInclude(p => p.Skill).FirstOrDefault(s => s.TaskApplicationId == id);
        }
        public List<TaskApplication> GetByTaskId(string taskId)
        {
            return _context.TaskApplications.Include(t=>t.Task).Include(ta=>ta.ProfessionalProfile).ThenInclude(p => p.Skills).ThenInclude(p => p.Skill).Include(ta => ta.ProfessionalProfile).ThenInclude(ta=>ta.User).Include(ta=>ta.ClientProfile).Where(t=>t.TaskId==taskId).ToList();
        }
        public List<TaskApplication> GetByProfessionalId(string professionalId)
        {
            return _context.TaskApplications.Include(ta => ta.Task).Include(ta=>ta.ProfessionalProfile).ThenInclude(p => p.Skills).ThenInclude(p=>p.Skill).Include(ta => ta.ProfessionalProfile).ThenInclude(cp=>cp.User).Include(ta=>ta.ClientProfile).Where(ta => ta.ProfessionalProfileId == professionalId).ToList();
        }
        public void Insert(TaskApplication application)
        {
            application.TaskApplicationId = Guid.NewGuid().ToString();
            _context.TaskApplications.Add(application);
        }
        public void Update(TaskApplication application) { 
            _context.TaskApplications.Update(application);
        }
        public void Delete(TaskApplication application) {
            _context.TaskApplications.Remove(application);
        }
    }
}
