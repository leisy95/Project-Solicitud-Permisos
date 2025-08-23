# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# 1️⃣ Copiar solo el csproj para restaurar dependencias
COPY PermisosApi.csproj ./
RUN dotnet restore

# 2️⃣ Copiar todo el código fuente
COPY . ./

# 3️⃣ Publicar en Release
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Exponer puerto 80
EXPOSE 80

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "PermisosApi.dll"]