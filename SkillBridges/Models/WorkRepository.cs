using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class WorkRepository:IWorkRepository
    {
        private readonly SkillBridgeContext _context;
        public WorkRepository(SkillBridgeContext context)
        {
            _context = context;
        }
        public WorkSubmission GetById(string id)
        {
            return _context.WorkSubmissions.FirstOrDefault(s => s.WorkSubmissionId == id);
        }
        public List<WorkSubmission> GetByTaskId(string taskId)
        {
            return _context.WorkSubmissions.Where(s => s.TaskId == taskId).ToList();
        }
        public void insert(WorkSubmission workSubmission)
        {
            workSubmission.WorkSubmissionId=Guid.NewGuid().ToString();
            _context.WorkSubmissions.Add(workSubmission);
        }
        public void update(WorkSubmission workSubmission)
        {
            _context.WorkSubmissions.Update(workSubmission);
        }
        public void delete(WorkSubmission workSubmission)
        {
            _context.WorkSubmissions.Remove(workSubmission);
        }
    }
}
