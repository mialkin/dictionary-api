# Dictionary API

## Run application

1\. Run application infrastructure:

```bash
docker-compose -f docker-compose.infrastructure.yml up
```

2\. Run application either by running `Dicrionary.Api` project in IDE  or by running Docker command:

```bash
docker run \
--detach \
--interactive \
--tty \
--publish 2100:80 \
--name dictionary-api \
mialkin/dictionary-api
```

3\.

// TODO Move migrations to docker container

// TODO Remove `Microsoft.EntityFrameworkCore.Design` package from Dictionary.Api project
```bash
PostgresSettings__ConnectionString="User ID=dictionary_api;Password=dictionary_api;Host=localhost;Port=2200;Database=dictionary_api;Pooling=true;Integrated Security=true" make migrate-database
```

4\. Navigate to <http://localhost:2100> in your web browser.

## Shutdown infrastructure

```bash
docker-compose -f docker-compose.infrastructure.yml down
```
