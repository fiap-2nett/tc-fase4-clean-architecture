#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/HelpDesk.ApiService.Api/HelpDesk.ApiService.Api.csproj", "src/HelpDesk.ApiService.Api/"]
RUN dotnet restore "src/HelpDesk.ApiService.Api/HelpDesk.ApiService.Api.csproj"
COPY . .
WORKDIR "/src/src/HelpDesk.ApiService.Api"
RUN dotnet build "HelpDesk.ApiService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelpDesk.ApiService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelpDesk.ApiService.Api.dll"]
