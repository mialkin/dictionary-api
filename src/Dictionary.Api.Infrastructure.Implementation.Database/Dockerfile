FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-environment

RUN dotnet tool install dotnet-ef --global
ENV PATH="$PATH:/root/.dotnet/tools"

COPY src ./src

ENV ASPNETCORE_ENVIRONMENT=GitLab

RUN dotnet ef migrations bundle \
    --startup-project src/Dictionary.Api \
    --project src/Dictionary.Api.Infrastructure.Implementation.Database \
    --self-contained \
    --runtime linux-x64 \
    --output /migration_bundle

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0

COPY --from=build-environment /migration_bundle ./

ENTRYPOINT ["./migration_bundle"]
