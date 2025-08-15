#!/bin/bash

# Parse command-line arguments
while getopts ":n:m:" opt; do
  case $opt in
    n) MIGRATION_NAME="$OPTARG"
    ;;
    m) MODULE_NAME="$OPTARG"
    ;;
    \?) echo "Invalid option -$OPTARG" >&2
    exit 1
    ;;
  esac
done

# Validate required parameters
if [ -z "$MIGRATION_NAME" ] || [ -z "$MODULE_NAME" ]; then
  echo "Usage: $0 -n MigrationName -m ModuleName"
  echo "Example: add-migration -n InitialCreate -m Identity"
  exit 1
fi

# Generate project paths
INFRA_PROJECT="src/Modules/$MODULE_NAME/Infrastructure/Cemiyet.Modules.$MODULE_NAME.Infrastructure.csproj"
STARTUP_PROJECT="src/Gateway/Cemiyet.Gateway.csproj"

# Run the command
dotnet ef migrations add "$MIGRATION_NAME" \
  --project "$INFRA_PROJECT" \
  --startup-project "$STARTUP_PROJECT"