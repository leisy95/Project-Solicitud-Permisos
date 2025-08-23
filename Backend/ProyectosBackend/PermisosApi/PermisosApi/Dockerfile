# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar solo el csproj desde la subcarpeta
COPY ./PermisosApi/PermisosApi.csproj ./PermisosApi/
WORKDIR /app/PermisosApi
RUN dotnet restore

# Copiar todo el código desde la subcarpeta
COPY ./PermisosApi/ ./ 

# Publicar en Release
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/PermisosApi/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]