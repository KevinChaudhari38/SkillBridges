using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class MessageRepository:IMessageRepository
    {
        private readonly SkillBridgeContext _context;
        public MessageRepository(SkillBridgeContext context)
        {
            _context = context;
        }
        public TaskMessage GetById(string id)
        {
            return _context.Messages.Include(t=>t.Task).FirstOrDefault(t => t.MessageId == id);
        }
        public List<TaskMessage> GetByTaskId(string TaskId)
        {
            return _context.Messages.Include(x=>x.Task).ThenInclude(y=>y.ClientProfile).Include(x=>x.Task).ThenInclude(z=>z.ProfessionalProfile).Where(t=>t.TaskId==TaskId).OrderByDescending(t=>t.SentAt).ToList();
        }
        public void insert(TaskMessage message)
        {
            
            message.MessageId=Guid.NewGuid().ToString();
            _context.Messages.Add(message);
            
            
        }
        public void update(TaskMessage message)
        {
            _context.Messages.Update(message);
        }
        public void delete(TaskMessage message)
        {
            _context.Messages.Remove(message);
        }
    }
}
