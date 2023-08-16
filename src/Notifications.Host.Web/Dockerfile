#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["global.json","."]
COPY ["AssemblyInfo.cs","."]
COPY ["src/Notifications.Host.Web/Notifications.Host.Web.csproj", "src/Notifications.Host.Web/"]
COPY ["src/ApplicationRegistry/ApplicationRegistry.csproj", "src/ApplicationRegistry/"]
COPY ["src/ApplicationRegistry.Contracts/ApplicationRegistry.Contracts.csproj", "src/ApplicationRegistry.Contracts/"]
COPY ["src/EventProcessor/EventProcessor.csproj", "src/EventProcessor/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
COPY ["src/Push/Push.csproj", "src/Push/"]
COPY ["src/Push.Contracts/Push.Contracts.csproj", "src/Push.Contracts/"]
COPY ["src/WebHooks/WebHooks.csproj", "src/WebHooks/"]
RUN dotnet restore "src/Notifications.Host.Web/Notifications.Host.Web.csproj"
COPY . .
WORKDIR "/src/src/Notifications.Host.Web"
RUN dotnet build "Notifications.Host.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notifications.Host.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notifications.Host.Web.dll"]