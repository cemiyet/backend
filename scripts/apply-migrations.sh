#!/bin/bash

# Parse command-line arguments
while getopts ":n:m:" opt; do
  case $opt in
    m) MODULE_NAME="$OPTARG"
    ;;
    \?) echo "Invalid option -$OPTARG" >&2
    exit 1
    ;;
  esac
done

# Validate required parameters
if [ -z "$MODULE_NAME" ]; then
  echo "Usage: $0 -m ModuleName"
  echo "Example: apply-migrations -m Identity"
  exit 1
fi

# Generate project paths
INFRA_PROJECT="src/Modules/$MODULE_NAME/Infrastructure/Cemiyet.Modules.$MODULE_NAME.Infrastructure.csproj"
STARTUP_PROJECT="src/Gateway/Cemiyet.Gateway.csproj"

# Run the command
dotnet ef database update \
  --project "$INFRA_PROJECT" \
  --startup-project "$STARTUP_PROJECT"