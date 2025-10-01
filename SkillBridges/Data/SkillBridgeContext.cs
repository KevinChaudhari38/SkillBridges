using Microsoft.EntityFrameworkCore;
using SkillBridges.Models;

namespace SkillBridges.Data
{
    public class SkillBridgeContext:DbContext
    {
        public SkillBridgeContext(DbContextOptions<SkillBridgeContext> options) : base(options) { }
        public DbSet<User> Users {  get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<ProfessionalProfile> ProfessionalProfiles { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<TaskApplication> TaskApplications { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ProfessionalSkill> ProfessionalSkills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TaskMessage> Messages {  get; set; }
        public DbSet<WorkSubmission>   WorkSubmissions { get; set; }
        public DbSet<Rating> Ratings {  get; set; }

        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProfessionalProfile>().HasOne(c => c.User).WithOne(u => u.ProfessionalProfile).HasForeignKey<ProfessionalProfile>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ClientProfile>().HasOne(c => c.User).WithOne(u => u.ClientProfile).HasForeignKey<ClientProfile>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
           
            modelBuilder.Entity<ProfessionalSkill>().HasKey(ps => new { ps.ProfessionalProfileId, ps.SkillId });
            modelBuilder.Entity<ProfessionalSkill>().HasOne(ps => ps.ProfessionalProfile).WithMany(p => p.Skills).HasForeignKey(ps=>ps.ProfessionalProfileId);
            modelBuilder.Entity<ProfessionalSkill>().HasOne(s=>s.Skill).WithMany(ps=>ps.ProfessionalSkills).HasForeignKey(p=>p.SkillId);

            modelBuilder.Entity<TaskApplication>().HasKey(ps =>ps.TaskApplicationId);
            modelBuilder.Entity<TaskApplication>().HasOne(ts=>ts.Task).WithMany(t=>t.TaskApplications).HasForeignKey(ts=>ts.TaskId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TaskApplication>().HasOne(ta => ta.ProfessionalProfile).WithMany(t => t.TaskApplications).HasForeignKey(p => p.ProfessionalProfileId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfessionalProfile>().HasMany(t => t.Tasks).WithOne(p => p.ProfessionalProfile).HasForeignKey(p => p.ProfessionalProfileId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ClientProfile>().HasMany(c=>c.TaskApplications).WithOne(c=>c.ClientProfile).HasForeignKey(p=>p.ClientProfileId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ClientProfile>().HasMany(t => t.Tasks).WithOne(t => t.ClientProfile).HasForeignKey(p => p.ClientProfileId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>().HasMany(t => t.Tasks).WithOne(c => c.Category).HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<TaskMessage>().HasOne(c => c.Task).WithMany().HasForeignKey(m => m.TaskId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Models.Task>().HasMany(c => c.Messages).WithOne(c => c.Task).HasForeignKey(p => p.TaskId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkSubmission>().HasOne(c=>c.Task).WithMany().HasForeignKey(c => c.TaskId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<WorkSubmission>().HasOne(c => c.ProfessionalProfile).WithMany().HasForeignKey(p => p.ProfessionalProfileId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Models.Task>().HasMany(c=>c.Messages).WithOne(c => c.Task).HasForeignKey(p => p.TaskId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rating>().HasOne(t => t.Task).WithOne().HasForeignKey<Rating>(t => t.TaskId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Rating>().HasOne(t => t.ClientProfile).WithMany().HasForeignKey(t => t.ClientProfileId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Rating>().HasOne(t => t.ProfessionalProfile).WithMany().HasForeignKey(t => t.ProfessionalProfileId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Models.Task>()
           .Property(t => t.Budjet)
            .HasPrecision(18, 2);

            modelBuilder.Entity<TaskApplication>()
    .Property(t => t.ExpectedBudjet)
    .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
           .HasOne(p => p.Task)
           .WithMany()
           .HasForeignKey(p => p.TaskId)
           .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.ClientProfile)
                .WithMany()
                .HasForeignKey(p => p.ClientProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.ProfessionalProfile)
                .WithMany()
                .HasForeignKey(p => p.ProfessionalProfileId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<User>().HasData(new User
            {
                Id = "admin",
                Name = "SkillBridge",
                Email = "bizzconnect2000@gmail.com",
                Password = "Skill@123",
                PhoneNumber="9265983497",
                Role = UserRole.Admin,
            });
            

        }

    }
}
