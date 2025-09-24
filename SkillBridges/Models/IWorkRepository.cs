namespace SkillBridges.Models
{
    public interface IWorkRepository
    {
        WorkSubmission GetById(string id);
        List<WorkSubmission> GetByTaskId(string taskId);
        void insert(WorkSubmission workSubmission);
        void update(WorkSubmission workSubmission);
        void delete(WorkSubmission workSubmission);
    }
}
