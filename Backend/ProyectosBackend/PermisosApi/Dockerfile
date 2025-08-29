# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiamos archivos de proyecto primero (mejor cache)
COPY PermisosApi/PermisosApi.csproj PermisosApi/
COPY PermisosApi.sln ./
RUN dotnet restore PermisosApi.sln

# Copiamos todo el código
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

# Railway define la variable PORT automáticamente
ENV ASPNETCORE_URLS=http://+:${PORT}

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermisosApi.dll"]
