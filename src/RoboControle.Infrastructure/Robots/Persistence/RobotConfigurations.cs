using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RoboControle.Domain.Entities.Robot;

namespace RoboControle.Infrastructure.Robots.Persistence;
public class RobotConfigurations : IEntityTypeConfiguration<RobotEntity>
{
    public void Configure(EntityTypeBuilder<RobotEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.ToString(),
                id => Ulid.Parse(id))
           .HasColumnName("ID");

        builder
            .Property(u => u.UserId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.ToString(),
                id => Ulid.Parse(id));

        builder.Property(u => u.Name);

        builder.OwnsOne(u => u.Head, head =>
        {
            head.Property(h => h.Inclination);
            head.Property(h => h.Rotation);
        });

        builder.OwnsOne(u => u.LeftArm, arm =>
        {
            arm.OwnsOne(a => a.Elbow, elbow =>
            {
                elbow.Property(e => e.Rotation);
                elbow.Ignore(e => e.IsStronglyContracted);
            });

            arm.OwnsOne(a => a.Wrist, wrist => wrist.Property(w => w.Rotation));
        });

        builder.OwnsOne(u => u.RightArm, arm =>
        {
            arm.OwnsOne(a => a.Elbow, elbow =>
            {
                elbow.Property(e => e.Rotation);
                elbow.Ignore(e => e.IsStronglyContracted);
            });

            arm.OwnsOne(a => a.Wrist, wrist => wrist.Property(w => w.Rotation));
        });
    }
}