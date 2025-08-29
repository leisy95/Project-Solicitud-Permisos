# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar archivos de proyecto primero
COPY PermisosApi/PermisosApi.csproj PermisosApi/
COPY PermisosApi.sln ./

RUN dotnet restore PermisosApi.sln

# Copiar todo el código
COPY . .

# Compilar
WORKDIR /src/PermisosApi
RUN dotnet build -c Release -o /app/build

# Etapa publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Etapa runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

# Railway asigna PORT automáticamente
ENV ASPNETCORE_URLS=http://+:${PORT}

ENTRYPOINT ["dotnet", "PermisosApi.dll"]

