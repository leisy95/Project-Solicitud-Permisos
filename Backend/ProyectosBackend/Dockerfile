# Imagen base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar archivo de solución (ajustando la ruta)
COPY PermisosApi/PermisosApi.sln ./ 

# Copiar el csproj
COPY PermisosApi/PermisosApi/PermisosApi.csproj PermisosApi/

RUN dotnet restore PermisosApi.sln

# Copiar el resto del proyecto
COPY PermisosApi/. ./PermisosApi/

WORKDIR /src/PermisosApi
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermisosApi.dll"]
