using Cemiyet.Modules.Identity.Domain;
using Cemiyet.Modules.Identity.Domain.Entities;
using Cemiyet.Modules.Identity.Domain.ValueObjects;
using Cemiyet.SharedKernel.Domain;
using Cemiyet.SharedKernel.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Modules.Identity.Infrastructure.Data;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<DomainEvent>();

        builder.HasDefaultSchema(ModuleConstants.SchemaName);

        builder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()"); // For PostgreSQL

            // Email mapping
            entity.Property(u => u.Email)
                .HasConversion(
                    v => v.Address,          // store as string
                    v => new Email(v))       // read as Email object
                .IsRequired();

            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.Property(u => u.RefreshToken)
                  .HasMaxLength(500)
                  .IsRequired(false);

            entity.Property(u => u.DisplayName).HasMaxLength(100);
            entity.Property(u => u.EmailConfirmed).IsRequired();

            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(u => u.UpdatedAt)
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();
        });

        builder.UseSnakeCaseNamingConvention();
    }
}
