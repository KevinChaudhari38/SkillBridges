using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;
using System.Runtime.CompilerServices;

namespace SkillBridges.Repositories
{
    public interface ITaskRepository
    {
        Models.Task GetById(string id);
        List<Models.Task> GetAll();
        List<Models.Task> GetByCategoryId(string id);
        List<Models.Task> GetByClientId(string clientId);
        List<Models.Task> GetByProfessionalId(string professionalId);
        List<Models.Task> GetByType(TaskType type);
        IEnumerable<SelectListItem> GetCities();
        void Insert(Models.Task task);
        void Update(Models.Task task);
        void Delete(Models.Task task);

    }
}
