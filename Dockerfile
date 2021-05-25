# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY GeolocalizacionIPs/*.sln .
COPY GeolocalizacionIPs/*.csproj .
RUN dotnet restore

# copy everything else and build app
COPY GeolocalizacionIPs/. .
WORKDIR /source
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "GeolocalizacionIPs.dll"]

## syntax=docker/dockerfile:1
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
#WORKDIR /app
#
## Copy csproj and restore as distinct layers
#COPY *.csproj ./
#RUN dotnet restore
#
## Copy everything else and build
#COPY bin/Release/netcoreapp3.1/publish/ ./
#RUN dotnet publish -c Release -o out
#
## Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:3.1
#WORKDIR /app
#COPY --from=build-env /app/out .
#ENTRYPOINT ["dotnet", "GeolocalizacionIPs.dll"]