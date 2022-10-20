using Microsoft.EntityFrameworkCore;
using StudyTimeManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimeManager.WPF.UI.ContextFactory
{
    /*public class RepositoryContextFactory
    {
         private readonly string _connectionString;

        public RepositoryContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RepositoryContext CreateDbContext()
        {
            DbContextOptionsBuilder<RepositoryContext> options =
                new DbContextOptionsBuilder<RepositoryContext>();
            options.UseSqlite(_connectionString);
            return new RepositoryContext(options.Options);
        }
    }*/
}
