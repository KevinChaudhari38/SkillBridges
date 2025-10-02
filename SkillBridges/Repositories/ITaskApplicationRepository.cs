using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public interface ITaskApplicationRepository
    {
        TaskApplication GetById(string id);
        List<TaskApplication> GetByTaskId(string taskId);
        List<TaskApplication> GetByProfessionalId(string professionalId);
        void Insert(TaskApplication taskApplication);
        void Update(TaskApplication taskApplication);
        void Delete(TaskApplication taskApplication);
    }
}
