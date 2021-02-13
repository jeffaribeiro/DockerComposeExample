#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DockerComposeExample.Api/DockerComposeExample.Api.csproj", "DockerComposeExample.Api/"]
COPY ["DockerComposeExample.Application/DockerComposeExample.Application.csproj", "DockerComposeExample.Application/"]
COPY ["DockerComposeExample.Domain/DockerComposeExample.Domain.csproj", "DockerComposeExample.Domain/"]
COPY ["DockerComposeExample.IoC/DockerComposeExample.IoC.csproj", "DockerComposeExample.IoC/"]
COPY ["DockerComposeExample.Repository/DockerComposeExample.Repository.csproj", "DockerComposeExample.Repository/"]
RUN dotnet restore "DockerComposeExample.Api/DockerComposeExample.Api.csproj"
COPY . .
WORKDIR "/src/DockerComposeExample.Api"
RUN dotnet build "DockerComposeExample.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerComposeExample.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerComposeExample.Api.dll"]
