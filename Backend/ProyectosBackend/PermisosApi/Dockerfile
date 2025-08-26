FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

<<<<<<< HEAD
# Copiar solo lo necesario
COPY PermisosApi.sln ./
COPY PermisosApi/ ./PermisosApi/

# Publicar directamente (restore impl�cito)
=======
# Copiar solución y proyecto
COPY PermisosApi.sln ./
COPY PermisosApi/ ./PermisosApi/

# Publicar (restore implícito)
>>>>>>> 2fe043621012bef9ec3e82de4e4c6ceb3a7e606a
RUN dotnet publish PermisosApi/PermisosApi.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]
// cambio para forzar rebuild
// forzar rebuild
