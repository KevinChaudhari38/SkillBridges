using Microsoft.EntityFrameworkCore;
using SkillBridges.Models;

namespace SkillBridges.Data
{
    public class SkillBridgeContext:DbContext
    {
        public SkillBridgeContext(DbContextOptions<SkillBridgeContext> options) : base(options) { }
        public DbSet<User> Users {  get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<ProfessionalProfile> ProfessionalProfiles { get;set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
