using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public interface ISkillRepository
    {
        Skill GetById(string id);
        Skill GetByName(string Name);
        List<Skill> GetByProfessionalId(string id);
        List<Skill> GetAll();
        void Insert(Skill skill);
        void Update(Skill skill);
        void Delete(Skill skill);
    }
}
