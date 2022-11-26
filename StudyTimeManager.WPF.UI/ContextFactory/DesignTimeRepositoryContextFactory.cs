using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudyTimeManager.Repository;
using System;
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

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativeDirectory = @"..\..\..\..\Database\";
            string absolutePath = Path.GetFullPath(Path.Combine(baseDirectory, relativeDirectory));
            string connectionString = configuration.GetConnectionString("sqlServerConnection")
                        .Replace("[DataDirectory]", absolutePath);

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(connectionString);

            /*
                        var builder = new DbContextOptionsBuilder<RepositoryContext>()
                            .UseSqlite(configuration.GetConnectionString("sqlConnection"),
                            b => b.MigrationsAssembly("StudyTimeManager.WPF.UI"));
            */
            return new RepositoryContext(builder.Options);
        }
    }
}
