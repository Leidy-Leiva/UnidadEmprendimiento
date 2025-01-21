# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV ConnectionDB="Server=sqlserverdocker,1433;Database=UNIDADEMPRENDIMIENTO;User Id=sa;Password=UEUA2025mj!*;MultipleActiveResultSets=True;TrustServerCertificate=True;"

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/Api.UnidadEmprendimiento/Api.UnidadEmprendimiento.csproj", "src/API/Api.UnidadEmprendimiento/"]
COPY ["src/Application/Api.UnidadEmprendimiento.Application/Api.UnidadEmprendimiento.Application.csproj", "src/Application/Api.UnidadEmprendimiento.Application/"]
COPY ["src/Data/Api.UnidadEmprendimiento.Data/Api.UnidadEmprendimiento.Data.csproj", "src/Data/Api.UnidadEmprendimiento.Data/"]
COPY ["src/Domain/Api.UnidadEmprendimiento.Domain/Api.UnidadEmprendimiento.Domain.csproj", "src/Domain/Api.UnidadEmprendimiento.Domain/"]
RUN dotnet restore "./src/API/Api.UnidadEmprendimiento/Api.UnidadEmprendimiento.csproj"
COPY . .
WORKDIR "/src/src/API/Api.UnidadEmprendimiento"
RUN dotnet build "./Api.UnidadEmprendimiento.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Instalar las herramientas de dotnet-ef
RUN dotnet tool install --global dotnet-ef


# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.UnidadEmprendimiento.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.UnidadEmprendimiento.dll"]