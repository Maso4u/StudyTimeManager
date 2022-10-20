using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudyTimeManager.Repository;
using System.IO;

namespace StudyTimeManager.WPF.UI.ContextFactory
{
    public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlite(configuration.GetConnectionString("sqlConnection"),
                b => b.MigrationsAssembly("StudyTimeManager.WPF.UI"));
            return new RepositoryContext(builder.Options);
        }
    }
}
