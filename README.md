# Prosperitopia
**Prosperitopia** is a Single Page Application (SPA) designed to manage items through a user-friendly interface. It provides functionality for creating, reading, updating, and deleting items. The backend is powered by an **ASP.NET Core Web API**, which serves as the data store for the items. The app utilizes API-Key authentication for secure access to the backend services.

## Functionality
### Backend
- Utilizes **Entity Framework** with  **In-Memory Database** as the initial data storage.
``` C#
services.AddDbContext<ProsperitopiaDbContext>(options => {
    //options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")); //comment out to configure to use SQL Server
    options.UseInMemoryDatabase("Prosperitopia");
});
```
- Authentication used is API-Key based via the `X-API-KEY` header. Allowed keys can be configured via `appsettings.json`.
``` JSON
  "RegisteredApiKeys": [
    "MWVjYTY0ZDItMTRkMC00YmRhLTgyZmItNTBkYmEzYTE0MjM1",
    "Y2YwNTJlMjQtYTBkOC00NzEwLTk4ZDAtYmE0MjE4NGU5MWNh",
    "ZjliYzU0ZDgtZTJmMC00MzVjLThjNmUtNThjZWNmN2RmMmY3"
  ],
```
### Frontend
- > Angular CLI: 17.3.0, Node: 18.18.0, Package Manager: npm 10.2.3
- The header `X-API-KEY` is passed on all Web API requests and can be configured in the `environment.ts`.
```typescript
export const environment = {
    BASE_URL: "http://localhost:7117/api",
    API_KEY: "MWVjYTY0ZDItMTRkMC00YmRhLTgyZmItNTBkYmEzYTE0MjM1"
};
```
- Utilizes Angular Material for the components.

# Installation

## Prerequisites
- Docker installed on your machine (for Docker setup, **[Suggested]** ). You can install **Docker Desktop** for [Windows](https://docs.docker.com/desktop/install/windows-install/), [Mac](https://docs.docker.com/desktop/install/mac-install/), or [Linux](https://docs.docker.com/desktop/install/linux-install/).
- .NET 6 SDK installed on your machine (for non-Docker setup).
- `Angular CLI: 17.3.0`, `Node: 18.18.0`, `Package Manager: npm 10.2.3` (for non-Docker setup).

## Docker Setup Steps

1. **Clone the Repositories:**
   Clone both the Web API and Angular repositories to your local machine.
   - Web: https://github.com/geomanyabes/Prosperitopia-Web
   - Web API: https://github.com/geomanyabes/Prosperitopia-Web-Api
2. **Navigate to the Directories:**
   Open your terminal or command prompt and navigate to the directories of the cloned repositories:
   ```
   cd path/to/Web
   cd path/to/WebApi
   ```

3. **Build Docker Images:** *(Skip to step 4 if you want to immediately run the container.)*

   In each directory, build the Docker images using the provided Dockerfiles.
   ```
   docker-compose build
   ```

4. **Run Docker Compose:**
   Once the images are built, navigate to the root directory where you have your `docker-compose.yml` file that orchestrates both services.
   ```
   cd path/to/root_directory_containing_docker_compose_file
   ```
5. **Start Services:**
   Run the following command to start both services defined in the `docker-compose.yml` file:
   ```
   docker-compose up
   ```
   NOTE:
   > The `docker-compose up` command reads the `docker-compose.yml` file and starts the services defined within it. It automatically builds the necessary images (if they don't already exist) and creates and runs the containers accordingly.

6. **Access the Applications:**
   - The Angular application will be accessible at `http://localhost:4200`.
   - The Web API will be accessible at `http://localhost:7117`.

7. **Stopping Services:**
   To stop the services, you can press `Ctrl + C` in the terminal where Docker Compose is running. To remove the container, you can run `docker compose down`.


For convenience, below is a composite `docker-compose.yml` file for running the containers for both frontend and backend:
   ```
   version: "3.4"
      name: prosperitopia
      services:
      prosperitopia-server:
         image: prosperitopia-server
         build:
            context: ./WebApi
            dockerfile: ./Prosperitopia.Web.Api/Dockerfile
         restart: unless-stopped
         ports:
            - "7117:7117"
      prosperitopia-web: 
         image: prosperitopia-web
         build:
            context: ./Web
            dockerfile: ./Dockerfile
         restart: unless-stopped
         ports:
            - "4200:80"
         depends_on:
            - prosperitopia-server
   ```
   For the above compose file to work, you need to checkout both repositories in the same directory so the structure will be as follows:

   ```
   root
   |--+ Web (checkout Web repository in this directory)
   |--+ WebApi (checkout Web API repository in this directory)
   |-- docker-compose.yml
   ```
   Navigate to the root directory and run `docker compose up`.
   ```
   cd path/to/root
   docker compose up
   ```
   Conversely, you can choose to run their individual `docker-compose.yml` manually as specified in the steps above.

## Non-Docker Setup Steps

1. **Clone the Repositories:**
   Clone both the Web API and Angular repositories to your local machine.

2. **Set Up Web API:**
   - Navigate to the `WebApi` directory.

   ```
   cd path/to/WebApi
   ```

   - Restore dependencies and build the Web API:
     ```
     dotnet restore
     dotnet build
     ```
   - Run the Web API:
     ```
     dotnet run --project Prosperitopia.Web.Api
     ```

3. **Set Up Angular Application:**
   - Navigate to the `Web` directory.
   - For consistency sake, make sure that the Node and NPM version are correct:
      ```
      node -v
      npm -v
      ```
      >  `Node: 18.18.0`, `Package Manager: npm 10.2.3`
   - Install Angular CLI globally if not already installed:
     ```
     npm install -g @angular/cli@17.3.0
     ```
   - confirm that Angular CLI is installed:
      ```
      ng version
      ```
   - Install dependencies and start the Angular application:
     ```
     npm install
     npm start
     ```

4. **Access the Applications:**
   - The Angular application will be accessible at `http://localhost:4200`.
   - The Web API will be accessible at `http://localhost:7117`.

## Note:
- Ensure that no other service is running on ports `4200` and `7117` as specified in the `docker-compose.yml` file to avoid conflicts. Change the ports in the `docker-compose.yml` if you can't free up ports **4200** and/or **7117**.
- Make sure Docker is running and properly configured on your machine if you choose to use Docker.

By following these steps, you should be able to run both the Web API and Angular application locally either with or without Docker.
