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

3\. Navigate to <http://localhost:2100> in your web browser.
