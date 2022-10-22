using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTimeManager.Domain.Models;
using System;

namespace StudyTimeManager.Repository.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = Guid.NewGuid(),
                    Username = "tester",
                    PasswordHash = ""
                });
        }
    }
}
