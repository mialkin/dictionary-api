# Dictionary API

## Run application

1\. Run application's infrastructure:

```bash
docker-compose -f docker-compose.infrastructure.yml up
```

2\. Run application in one of these ways:

2\.1 In IDE, by launching `Dicrionary.Api` project

2\.2 In Docker, by running command:

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
