using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Extensions;
using Cemiyet.Persistence.Application.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cemiyet.Persistence.Application.Contexts
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EntityChange> EntityChanges { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookEdition> BookEditions { get; set; }
        public DbSet<Serie> Series { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityChangeConfiguration());
            modelBuilder.ApplyConfiguration(new DimensionConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookEditionConfiguration());
            modelBuilder.ApplyConfiguration(new BooksGenresConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorsBooksConfiguration());
            modelBuilder.ApplyConfiguration(new SerieConfiguration());
            modelBuilder.ApplyConfiguration(new SeriesBooksConfiguration());

            // use snake case naming convention
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    var storeObjectIdentifier = StoreObjectIdentifier.Create((IMutableEntityType)property.DeclaringType, StoreObjectType.Table);
                    property.SetColumnName(property.GetColumnName(storeObjectIdentifier.GetValueOrDefault()).ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                    key.SetName(key.GetName().ToSnakeCase());

                foreach (var index in entity.GetIndexes())
                    index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
            }
        }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker
                                   .Entries().Where(e => !(e.Entity is EntityChange) && e.State == EntityState.Modified)
                                   .ToList();

            LogEntityChanges(modifiedEntities);

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modifiedEntities = ChangeTracker
                                   .Entries().Where(e => !(e.Entity is EntityChange) && e.State == EntityState.Modified)
                                   .ToList();

            LogEntityChanges(modifiedEntities);

            return base.SaveChangesAsync(cancellationToken);
        }

        private void LogEntityChanges(IEnumerable<EntityEntry> entities)
        {
            var modificationDate = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                // todo: currently can't log values without Id property. (like book editions)
                if (!entity.IsKeySet || entity.Properties.All(x => x.Metadata.Name != "Id")) continue;

                var entityId = entity.OriginalValues["Id"].ToString();
                var properties = entity.OriginalValues.Properties.Where(p => p.Name != "ModificationDate").ToList();

                foreach (var property in properties)
                {
                    var originalValue = entity.OriginalValues[property]?.ToString();
                    var currentValue = entity.CurrentValues[property]?.ToString();

                    if (originalValue == currentValue) continue;

                    var ec = new EntityChange
                    {
                        EntityId = new Guid(entityId),
                        PropertyName = property.Name,
                        OldValue = originalValue,
                        NewValue = currentValue,
                        ModificationDate = modificationDate
                        // TODO (v0.5): create relations with user model.
                        // ModifierId =
                    };
                    EntityChanges.Add(ec);
                }
            }
        }
    }
}
