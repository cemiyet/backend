using Cemiyet.SharedKernel.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cemiyet.SharedKernel.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (IMutableEntityType entity in builder.Model.GetEntityTypes())
        {
            // Convert table name
            entity.SetTableName(entity.GetTableName()!.ToSnakeCase());

            // Convert column names
            foreach (IMutableProperty property in entity.GetProperties())
                property.SetColumnName(property.Name.ToSnakeCase());

            // Convert key names
            foreach (IMutableKey key in entity.GetKeys())
                key.SetName(key.GetName()!.ToSnakeCase());

            // Convert foreign keys
            foreach (IMutableForeignKey fk in entity.GetForeignKeys())
                fk.SetConstraintName(fk.GetConstraintName()!.ToSnakeCase());

            // Convert indexes
            foreach (IMutableIndex index in entity.GetIndexes())
                index.SetDatabaseName(index.GetDatabaseName()!.ToSnakeCase());

            // Convert derived and owned types
            IEnumerable<IMutableEntityType> derivedTypes = entity.GetDerivedTypes();

            IEnumerable<IMutableEntityType> ownedTypes = entity.GetNavigations()
                .Where(n => n.TargetEntityType.IsOwned())
                .Select(n => n.TargetEntityType);

            foreach (IMutableEntityType? owned in derivedTypes.Concat(ownedTypes))
            {
                foreach (IMutableProperty property in owned.GetProperties())
                {
                    StoreObjectIdentifier tableId = StoreObjectIdentifier.Table(entity.GetTableName()!, entity.GetSchema());
                    property.SetColumnName(property.GetColumnName(tableId)!.ToSnakeCase());
                }

                foreach (IMutableIndex index in owned.GetIndexes())
                    index.SetDatabaseName(index.GetDatabaseName()!.ToSnakeCase());
            }
        }
    }

}
