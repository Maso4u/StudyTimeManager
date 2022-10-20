using Microsoft.EntityFrameworkCore;

namespace StudyTimeManager.Repository.ContextFactory
{
    public class RepositoryContextFactory
    {
        private readonly string _connectionString;

        public RepositoryContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RepositoryContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlite(_connectionString,
                b => b.MigrationsAssembly("StudyTimeManager.WPF.UI"))
                .Options;

            /*DbContextOptionsBuilder<RepositoryContext> options =
                new DbContextOptionsBuilder<RepositoryContext>();*/
            
            return new RepositoryContext(options);
        }
    }
}
