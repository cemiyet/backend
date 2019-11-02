using Cemiyet.Core.Entities;
using Cemiyet.Core.Extensions;
using Cemiyet.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Persistence.Contexts
{
    public class MainDataContext : DbContext
    {
        public MainDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DimensionConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());

            // use snake case naming convention
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());

                foreach (var key in entity.GetKeys())
                    key.SetName(key.GetName().ToSnakeCase());

                foreach (var index in entity.GetIndexes())
                    index.SetName(index.GetName().ToSnakeCase());
            }
        }
    }
}
