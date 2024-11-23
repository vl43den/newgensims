# Step 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT=Development
# Copy the UserApi project file and restore dependencies
COPY UserApi/UserApi.csproj UserApi/
RUN dotnet restore "UserApi/UserApi.csproj"

# Copy the rest of the application files (ensure the files are placed in the correct directories)
COPY . .

# Set the correct working directory for the UserApi project
WORKDIR /src/UserApi

# Publish the application to the /app/publish directory
RUN dotnet publish "UserApi.csproj" -c Release -o /app/publish

# Step 2: Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Expose the application port
EXPOSE 5000

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "UserApi.dll"]
