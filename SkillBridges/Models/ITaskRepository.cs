using System.Runtime.CompilerServices;

namespace SkillBridges.Models
{
    public interface ITaskRepository
    {
        Task GetById(string id);
        List<Task> GetAll();
        List<Task> GetByCategoryId(string id);
        List<Task> GetByClientId(string clientId);
        void Insert(Task task);
        void Update(Task task);
        void Delete(Task task);

    }
}
