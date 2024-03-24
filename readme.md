# Dictionary API

## Running application

### Global prerequisites

Install [↑ Docker](https://www.docker.com).

### Running application in Docker

TODO

### Running application in IDE

1\. Install prerequisites:

1. [↑ GNU Make](https://www.gnu.org/software/make)
2. [↑ .NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
3. [↑ EF Core command-line tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

2\. Run application infrastructure:

```bash
make run-infrastructure

# Shutdown infrastructure:
# make shutdown-infrastructure
```

3\. Set user secret:

```bash
dotnet user-secrets set "PostgresSettings:ConnectionString" "User ID=dictionary_api;Password=dictionary_api;Host=localhost;Port=3300;Database=dictionary_api" --project "src/Dictionary.Api"

# List and clear secrets:
# dotnet user-secrets list --project "src/Dictionary.Api"
# dotnet user-secrets clear --project "src/Dictionary.Api"
```

4\. Apply database migrations:

```bash
make update-database
```

5\. Run application:

```bash
dotnet run --project "src/Dictionary.Api"
```

6\. Navigate to <http://localhost:2300>.

[//]: # (// TODO Add command that sets everything up and runs app in Docker?)

[//]: # ()
[//]: # (// TODO Move migrations to docker container)

[//]: # ()
[//]: # (// TODO Remove `Microsoft.EntityFrameworkCore.Design` package from Dictionary.Api project)
