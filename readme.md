# Dictionary API

## Prerequisites

- [↑ Docker](https://www.docker.com)
- [↑ GNU Make](https://www.gnu.org/software/make)
- [↑ .NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [↑ EF Core command-line tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)



## Run application

1\. Run application infrastructure:

```bash
make run-infrastructure

# make shutdown-infrastructure
```

2\.

Set user secret:

```bash
dotnet user-secrets set "PostgresSettings:ConnectionString" "User ID=dictionary_api;Password=dictionary_api;Host=localhost;Port=2200;Database=dictionary_api" --project "src/Dictionary.Api"

# dotnet user-secrets list --project "src/Dictionary.Api"
# dotnet user-secrets clear --project "src/Dictionary.Api"
```

3\. Apply database migrations:

```bash
make update-database
```

4\. Run application:

```bash
dotnet run --project "src/Dictionary.Api"
```

5\. Navigate to <http://localhost:2100>.

[//]: # (// TODO Add command that sets everything up and runs app in Docker?)

[//]: # ()
[//]: # (// TODO Move migrations to docker container)

[//]: # ()
[//]: # (// TODO Remove `Microsoft.EntityFrameworkCore.Design` package from Dictionary.Api project)
