namespace Cemiyet.Modules.Identity.Domain;

/// <summary>
/// Module constants for the Identity module.
/// This class contains constants used throughout the Identity module, such as connection strings,
/// migration history table names, and schema names.
/// These constants help maintain consistency and avoid hardcoding values in multiple places.
/// The values can be configured in the application settings or environment variables.
///
/// Example usage:
/// var connectionString = configuration.GetConnectionString(ModuleConstants.ConnectionStringName);
/// var migrationsHistoryTable = ModuleConstants.MigrationsHistoryTable;
/// </summary>
public static class ModuleConstants
{
    public const string ModuleName = "Identity";
    public const string ConnectionStringName = "IdentityDatabase";
    public const string MigrationsHistoryTable = "__EFMigrationsHistory";
    public const string SchemaName = "identity";
}
