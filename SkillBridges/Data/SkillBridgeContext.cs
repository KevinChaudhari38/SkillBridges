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
            modelBuilder.Entity<ClientProfile>().HasOne(c => c.User).WithOne(u => u.ClientProfile).HasForeignKey<ClientProfile>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProfessionalProfile>().HasOne(c => c.User).WithOne(u => u.ProfessionalProfile).HasForeignKey<ProfessionalProfile>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
        }

    }
}
