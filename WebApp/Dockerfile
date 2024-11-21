# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore the dependencies
COPY ["WebApp/WebApp.csproj", "WebApp/"]
RUN dotnet restore "WebApp/WebApp.csproj"

# Publish the app to /app in release mode
COPY . .
WORKDIR "/src/WebApp"
RUN dotnet publish "WebApp.csproj" -c Release -o /app

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published app from the build image
COPY --from=build /app .

# Set the entry point for the container to run the app
ENTRYPOINT ["dotnet", "WebApp.dll", "--urls", "http://0.0.0.0:80"]

