FROM mialkin/dotnet-sdk-with-tools:8.0.203 AS build-environment

WORKDIR /application
COPY . ./

ENV ASPNETCORE_ENVIRONMENT=GitLab

RUN dotnet ef migrations bundle \
    --startup-project src/Dictionary.Api \
    --project src/Dictionary.Api.Infrastructure.Implementation.Database \
    --self-contained \
    --runtime linux-x64 \
    --output /migration_bundle

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0.3

WORKDIR /application
COPY --from=build-environment /migration_bundle ./

ENTRYPOINT ["./migration_bundle"]
