# MassTransit Example

An simple example solution for a distributed event-driven system build with MassTransit using Mongo DB for saga persistence and a transactional outbox.

## Run

To run the application use docker-compose:

```bash
docker-compose up -d # optionally pass the --build flag if you want to rebuild images
```

> Please note that the solution assumes you already have a Mongo DB and Rabbit MQ instance running locally. Use the root `docker-compose` to get these components running