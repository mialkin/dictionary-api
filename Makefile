export PROJECT=Dictionary.Api.Infrastructure.Implementation.Database
export STARTUP_PROJECT=Dictionary.Api

.PHONY: copy-env
copy-env:
	cp -n .env.example .env | true

.PHONY: run-infrastructure
run-infrastructure: copy-env
	docker-compose -f docker-compose.infrastructure.yml up

.PHONY: shutdown-infrastructure
shutdown-infrastructure:
	docker-compose -f docker-compose.infrastructure.yml down

.PHONY: update-database
update-database:
	dotnet ef database update --project src/${PROJECT} --startup-project src/${STARTUP_PROJECT}
