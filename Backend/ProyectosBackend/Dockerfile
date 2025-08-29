# Imagen base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY PermisosApi/PermisosApi.sln ./
COPY PermisosApi/PermisosApi/PermisosApi.csproj PermisosApi/

RUN dotnet restore PermisosApi.sln

COPY PermisosApi/. ./PermisosApi/

WORKDIR /src/PermisosApi
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermisosApi.dll"]
