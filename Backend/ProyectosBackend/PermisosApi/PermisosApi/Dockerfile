# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# 1️⃣ Copiar solo el csproj y restaurar dependencias
COPY PermisosApi.csproj ./
RUN dotnet restore

# 2️⃣ Copiar todo el código
COPY . ./

# 3️⃣ Publicar en Release
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copiar la salida del build
COPY --from=build /app/out .

# Exponer puerto 80
EXPOSE 80

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "PermisosApi.dll"]