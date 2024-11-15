# NewGenSims Project

## Project Description
A system for logging IT security incidents with full Docker-based deployment. It includes a Blazor Web App, a REST API for user management, a relational database for incident storage, and Redis for caching.

## Features
- Incident logging (manual entry, severity, status, etc.)
- User management (CRUD operations)
- Logging system with roles and access control

## System Requirements
- Docker
- .NET 8 runtime
- SQL Server
- Redis

## How to Run
1. Clone the repo
2. Run `docker-compose up --build`
3. Access the WebApp at `http://localhost`
4. API available at `http://localhost:5000`

## Docker Configuration
- `docker-compose.yml` for container orchestration
- Each service (WebApp, UserApi, Redis, MSSQL) runs in a separate container

## License
MIT License

## Contributors
- vl43den
- JP
- stdla
- shxl
