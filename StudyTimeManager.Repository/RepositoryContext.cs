using Microsoft.EntityFrameworkCore;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Configuration;

namespace StudyTimeManager.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Semester>? Semesters { get; set; }
        public DbSet<Module>? Modules { get; set; }
        public DbSet<ModuleSemesterWeek>? ModuleSemesterWeeks { get; set; }
        public DbSet<StudySession>? StudySessions { get; set; }

    }
}
