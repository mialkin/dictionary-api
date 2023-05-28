# Dictionary API

## Run application

1\. Run application's infrastructure:

```bash
docker-compose -f docker-compose.infrastructure.yml up
```

2\. Run application either in IDE  by running `Dicrionary.Api` project in Rider or run it in Docker:

```bash
docker run \
--detach \
--interactive \
--tty \
--publish 2100:5000 \
--name dictionary-api \
mialkin/dictionary-api
```

3\. Navigate to <http://localhost:2100> in your web browser.

