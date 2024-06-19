ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

FROM ${DOTNET_RUNTIME} AS base
ENV ASPNETCORE_URLS=http://+:7105
WORKDIR /home/app
EXPOSE 7105

FROM ${DOTNET_SDK} AS build
WORKDIR /src
COPY ["ExpenseManager.sln", "./"]
COPY ["src/ExpenseManager.Presentation/ExpenseManager.Presentation.csproj", "src/ExpenseManager.Presentation/ExpenseManager.Presentation.csproj"]
COPY ["src/ExpenseManager.Infrastructure/ExpenseManager.Infrastructure.csproj", "src/ExpenseManager.Infrastructure/ExpenseManager.Infrastructure.csproj"]
COPY ["src/ExpenseManager.Domain/ExpenseManager.Domain.csproj", "src/ExpenseManager.Domain/ExpenseManager.Domain.csproj"]
COPY ["src/ExpenseManager.Application/ExpenseManager.Application.csproj", "src/ExpenseManager.Application/ExpenseManager.Application.csproj"]
COPY ["tests/UnitTests/ExpenseManager.Presentation.UnitTests/ExpenseManager.Presentation.UnitTests.csproj", "tests/UnitTests/ExpenseManager.Presentation.UnitTests/ExpenseManager.Presentation.UnitTests.csproj"]
COPY ["tests/UnitTests/ExpenseManager.Infrastructure.UnitTests/ExpenseManager.Infrastructure.UnitTests.csproj", "tests/UnitTests/ExpenseManager.Infrastructure.UnitTests/ExpenseManager.Infrastructure.UnitTests.csproj"]
COPY ["tests/UnitTests/ExpenseManager.Domain.UnitTests/ExpenseManager.Domain.UnitTests.csproj", "tests/UnitTests/ExpenseManager.Domain.UnitTests/ExpenseManager.Domain.UnitTests.csproj"]
COPY ["tests/UnitTests/ExpenseManager.Application.UnitTests/ExpenseManager.Application.UnitTests.csproj", "tests/UnitTests/ExpenseManager.Application.UnitTests/ExpenseManager.Application.UnitTests.csproj"]
RUN dotnet restore ExpenseManager.sln
COPY ["src/", "src/"]

## Run migrations
FROM build as migrations
RUN dotnet tool install --version 6.0.9 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update -p src/ExpenseManager.Infrastructure/ -s src/ExpenseManager.Presentation/