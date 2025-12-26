# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY src/Structura.Api/Structura.Api.csproj ./Structura.Api/
COPY src/Structura.Application/Structura.Application.csproj ./Structura.Application/
COPY src/Structura.Domain/Structura.Domain.csproj ./Structura.Domain/
COPY src/Structura.Infrastructure/Structura.Infrastructure.csproj ./Structura.Infrastructure/
RUN dotnet restore Structura.Api/Structura.Api.csproj

# Copy all source files and build
COPY . .
WORKDIR /src/src/Structura.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Set environment variables
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV DOTNET_PRINT_TELEMETRY_MESSAGE=false

# Expose port
EXPOSE 80
EXPOSE 443

# Start the app
ENTRYPOINT ["dotnet", "Structura.Api.dll"]