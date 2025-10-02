using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public interface IMessageRepository
    {
        TaskMessage GetById(string id);
        List<TaskMessage> GetByTaskId(string TaskId);
        void insert(TaskMessage message);
        void update(TaskMessage message);
        void delete(TaskMessage message);

    }
}
