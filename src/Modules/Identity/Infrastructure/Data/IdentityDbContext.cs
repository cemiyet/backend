using Cemiyet.Modules.Identity.Domain.Entities;
using Cemiyet.SharedKernel.Domain;
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

            entity.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Address)
                    .HasColumnName("email")
                    .IsRequired();

                email.HasIndex(e => e.Address)
                    .IsUnique();
            });

            entity.Property(u => u.DisplayName).HasMaxLength(100);
            entity.Property(u => u.EmailConfirmed).IsRequired();
            entity.Property(u => u.CreatedAt).IsRequired();
        });

    }
}