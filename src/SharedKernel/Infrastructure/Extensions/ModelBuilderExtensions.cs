using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cemiyet.SharedKernel.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    private static readonly Regex _regex = new(@"([a-z0-9])([A-Z])", RegexOptions.Compiled);

    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (IMutableEntityType entity in builder.Model.GetEntityTypes())
        {
            // Convert table name
            entity.SetTableName(ToSnakeCase(entity.GetTableName()!));

            // Convert column names
            foreach (IMutableProperty property in entity.GetProperties())
                property.SetColumnName(ToSnakeCase(property.Name));

            // Convert key names
            foreach (IMutableKey key in entity.GetKeys())
                key.SetName(ToSnakeCase(key.GetName()!));

            // Convert foreign keys
            foreach (IMutableForeignKey fk in entity.GetForeignKeys())
                fk.SetConstraintName(ToSnakeCase(fk.GetConstraintName()!));

            // Convert indexes
            foreach (IMutableIndex index in entity.GetIndexes())
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));

            IEnumerable<IMutableEntityType> derivedTypes = entity.GetDerivedTypes();

            IEnumerable<IMutableEntityType> ownedTypes = entity.GetNavigations()
                .Where(n => n.TargetEntityType.IsOwned())
                .Select(n => n.TargetEntityType);

            foreach (IMutableEntityType? owned in derivedTypes.Concat(ownedTypes))
            {
                foreach (IMutableProperty property in owned.GetProperties())
                {
                    StoreObjectIdentifier tableId = StoreObjectIdentifier.Table(entity.GetTableName()!, entity.GetSchema());
                    property.SetColumnName(ToSnakeCase(property.GetColumnName(tableId)!));
                }

                foreach (IMutableIndex index in owned.GetIndexes())
                    index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));
            }
        }
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return _regex.Replace(input, "$1_$2").ToLowerInvariant();
    }
}
