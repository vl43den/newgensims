# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/UserApi

# Copy the project file and restore dependencies
COPY UserApi.csproj .
RUN dotnet restore "UserApi.csproj"

# Copy the rest of the source code
COPY . .

# Publish the app 
RUN dotnet publish "UserApi.csproj" -c Release -o /app/publish

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published app from the build container
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserApi.dll"]
