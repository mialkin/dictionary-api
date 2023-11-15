export MIGRATE_STARTUP_PROJECT_KEY=Dictionary.Api
export MIGRATE_PROJECT_KEY=Dictionary.Api.Infrastructure.Implementation.Database

.PHONY: copy-env
copy-env:
	cp -n .env.example .env | true

.PHONY: run-infrastructure
run-infrastructure: copy-env
	docker-compose -f docker-compose.infrastructure.yml up

.PHONY: shutdown-infrastructure
shutdown-infrastructure:
	docker-compose -f docker-compose.infrastructure.yml down

.PHONY: migrate-database
migrate-database:
	dotnet ef database update --startup-project src/${MIGRATE_STARTUP_PROJECT_KEY} --project src/${MIGRATE_PROJECT_KEY}
