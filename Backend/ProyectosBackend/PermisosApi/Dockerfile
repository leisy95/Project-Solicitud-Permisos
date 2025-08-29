# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiamos los archivos de proyecto primero
COPY PermisosApi/PermisosApi.csproj PermisosApi/
COPY PermisosApi.sln ./

# Restauramos dependencias
RUN dotnet restore PermisosApi.sln

# Copiamos el resto del código
COPY . .

# Compilamos
WORKDIR /src/PermisosApi
RUN dotnet build PermisosApi.csproj -c Release -o /app/build

# Etapa publish
FROM build AS publish
RUN dotnet publish PermisosApi.csproj -c Release -o /app/publish

# Etapa runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermisosApi.dll"]