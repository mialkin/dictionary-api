FROM mcr.microsoft.com/dotnet/sdk:8.0.203 AS build-environment

WORKDIR /application
COPY . ./

RUN dotnet restore
RUN dotnet publish --configuration Release --output release

FROM mcr.microsoft.com/dotnet/aspnet:8.0.3

WORKDIR /application
COPY --from=build-environment /application/release .

ENTRYPOINT ["./Dictionary.Api"]
