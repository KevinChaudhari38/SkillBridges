using SkillBridges.Data;

namespace SkillBridges.Models
{
    public interface IClientRepository
    {
        ClientProfile GetById(string id);
        ClientProfile GetByUserId(string userId);
        List<ClientProfile> GetAll();
        void insert(ClientProfile clientProfile);
        void update(ClientProfile clientProfile);
        void delete(ClientProfile clientProfile);
        
    }
}
