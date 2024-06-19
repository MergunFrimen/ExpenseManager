# Expense Manager

## How to run

Requires Docker and Docker Compose.

### Docker

This is the easiest way to run the application. It will build the frontend and backend and run them in separate containers.

```bash
docker-compose up --build
```

### Local

Will require running a Postgres database first. Can do this with Docker:
```bash
docker run -it --rm -e POSTGRES_PASSWORD=postgres -p 5432:5432 postgres:alpine
````

Then run migrations:
```bash
dotnet ef database update -p src/ExpenseManager.Infrastructure -s src/ExpenseManager.Presentation --connection "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=postgres"
```

Run the web API:
```bash
dotnet build
dotnet run --project src/ExpenseManager.Presentation/
```

Run the frontend:
```bash
cd src/ExpenseManager.Presentation/Client
npm i
npm run build
npm run preview
```

## Sources

- [Lukáš Grolig](https://www.youtube.com/watch?v=E7vQ8Rvq2rw)

- [Amichai Mantinband](https://www.youtube.com/@amantinband/)

- [Jason Taylor](https://www.youtube.com/watch?v=dK4Yb6-LxAk)

- [Remigiusz Zalewski](https://www.youtube.com/@remigiuszzalewski)
