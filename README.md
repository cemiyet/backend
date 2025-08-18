<h1 align="center">
  <br>
  <img src="logo.svg" alt="Cemiyet">
  <br>
  <br>
  <br>
</h1>

# Cemiyet Backend Repository

Install project templates with:

> dotnet new install ./templates

Make scripts executable:

> chmod +x scripts/\*.sh

Create new module with:

> dotnet new cemiyet-module -n ModuleName -s ../Cemiyet.sln --output ./src/Modules -v diag

Create a migration with:

> scripts/add-module.sh -n MigrationName -m ModuleName

Apply migrations with:

> scripts/apply-migrations.sh -m ModuleName

Run components (db etc.) with:

> podman compose up -d
