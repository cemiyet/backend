#!/bin/bash

set -euo pipefail

if [ -z "${1-}" ]; then
  echo "Usage: $0 ModuleName"
  exit 1
fi

MODULE=$1
BASE_DIR="src/Modules/$MODULE"
NAMESPACE_PREFIX="X.Modules.$MODULE"
LAYERS=(Domain Application Infrastructure)
# Uncomment if you want API layer
LAYERS+=(API)

echo "Creating module: $MODULE"

echo "Creating folders..."
for layer in "${LAYERS[@]}"; do
  mkdir -p "$BASE_DIR/$layer"
done

echo "Creating projects..."
for layer in "${LAYERS[@]}"; do
  if [ "$layer" == "API" ]; then
    dotnet new webapi -n "$NAMESPACE_PREFIX.API" -o "$BASE_DIR/API"
  else
    dotnet new classlib -n "$NAMESPACE_PREFIX.$layer" -o "$BASE_DIR/$layer"
  fi
done


set_root_namespace() {
  local csproj_path=$1
  local root_ns=$2

  if ! command -v dotnet >/dev/null 2>&1; then
    echo "Error: dotnet CLI not found"
    exit 1
  fi

  dotnet run --no-build --project tools/CsprojRootNamespaceSetter -- "$csproj_path" "$root_ns"
}

echo "Setting RootNamespace..."
for layer in "${LAYERS[@]}"; do
  CSPROJ_PATH="$BASE_DIR/$layer/$NAMESPACE_PREFIX.$layer.csproj"
  set_root_namespace "$CSPROJ_PATH" "$NAMESPACE_PREFIX.$layer"
done

echo "Adding projects to solution..."
for layer in "${LAYERS[@]}"; do
  CSPROJ_PATH="$BASE_DIR/$layer/$NAMESPACE_PREFIX.$layer.csproj"
  dotnet sln add "$CSPROJ_PATH"
done

echo "Module $MODULE created successfully!"
