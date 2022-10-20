using Microsoft.EntityFrameworkCore;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Semester>? Semesters { get; set; }
        public DbSet<Module>? Modules { get; set; }
        public DbSet<ModuleSemesterWeek>? ModuleSemesterWeeks { get; set; }
        public DbSet<StudySession>? StudySessions { get; set; }

    }
}
