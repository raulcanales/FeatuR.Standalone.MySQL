#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/FeatuR.Standalone.MySQL.WebApi/FeatuR.Standalone.MySQL.WebApi.csproj", "src/FeatuR.Standalone.MySQL.WebApi/"]
RUN dotnet restore "src/FeatuR.Standalone.MySQL.WebApi/FeatuR.Standalone.MySQL.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FeatuR.Standalone.MySQL.WebApi"
RUN dotnet build "FeatuR.Standalone.MySQL.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FeatuR.Standalone.MySQL.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeatuR.Standalone.MySQL.WebApi.dll"]