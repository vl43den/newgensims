# Step 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY UserApi/UserApi.csproj UserApi/
RUN dotnet restore "UserApi/UserApi.csproj"

# Copy the rest of the app's files
COPY . .

WORKDIR /src/UserApi

# Publish the app to the /app/publish directory
RUN dotnet publish "UserApi.csproj" -c Release -o /app/publish

# Step 2: Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Expose the port
EXPOSE 5000

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "UserApi.dll"]
