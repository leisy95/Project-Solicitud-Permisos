FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solo lo necesario
COPY PermisosApi.sln ./
COPY PermisosApi/ ./PermisosApi/

# Publicar directamente (restore implícito)
RUN dotnet publish PermisosApi/PermisosApi.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]