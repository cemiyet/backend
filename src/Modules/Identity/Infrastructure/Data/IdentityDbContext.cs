using Cemiyet.Modules.Identity.Domain.Entities;
using Cemiyet.Modules.Identity.Domain.ValueObjects;
using Cemiyet.SharedKernel.Domain;
using Cemiyet.SharedKernel.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Modules.Identity.Infrastructure.Data;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Ignore<DomainEvent>();

        // TODO: manage schemas without magic strings, maybe with enums or constants
        builder.HasDefaultSchema("identity");

        builder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()"); // For PostgreSQL

            entity.Property(u => u.Email)
                .HasConversion(
                    v => v.Address,          // store as string
                    v => new Email(v))       // read as Email object
                .IsRequired();

            entity.HasIndex(u => u.Email)
                .IsUnique();

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