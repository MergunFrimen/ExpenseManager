ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

FROM ${DOTNET_RUNTIME} AS base
WORKDIR /home/app
ENV ASPNETCORE_URLS=http://+:5222
EXPOSE 5222

FROM ${DOTNET_SDK} AS build
WORKDIR /app
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
# RUN dotnet build ExpenseManager.sln -c Release -o /app/build

#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /app
#COPY . .
#RUN dotnet restore ExpenseManager.sln
#RUN dotnet build ExpenseManager.sln -c Release -o /app/build

FROM build AS publish
RUN dotnet publish ExpenseManager.sln -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 5222
ENV ASPNETCORE_HTTP_PORTS=5222
ENV ASPNETCORE_URLS=http://+:5222
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpenseManager.Presentation.dll"]
