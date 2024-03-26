using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RoboControle.Domain.Entities;

namespace RoboControle.Infrastructure.Users.Persistence;
public class RobotConfigurations : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.ToString(),
                id => Ulid.Parse(id));

        builder.Property(u => u.Email);

        builder.Property(u => u.LastName);

        builder.Property(u => u.FirstName);

        builder.Property(u => u.PasswordSalt);

        builder.Property(u => u.PasswordHash);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}