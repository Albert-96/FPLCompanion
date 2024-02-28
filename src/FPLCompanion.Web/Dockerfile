#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FPLCompanion.Web/FPLCompanion.csproj", "src/FPLCompanion.Web/"]
COPY ["src/FPLCompanion.HostedServices/FPLCompanion.HostedServices.csproj", "src/FPLCompanion.HostedServices/"]
COPY ["src/FPLCompanion.DataService/FPLCompanion.DataService.csproj", "src/FPLCompanion.DataService/"]
COPY ["src/FPLCompanion.Data/FPLCompanion.Data.csproj", "src/FPLCompanion.Data/"]
COPY ["src/FPLCompanion.ApplicationServices/FPLCompanion.ApplicationServices.csproj", "src/FPLCompanion.ApplicationServices/"]
COPY ["src/FPLCompanion.Dependencies/FPLCompanion.Dependencies.csproj", "src/FPLCompanion.Dependencies/"]
COPY ["src/FPLCompanion.Dto/FPLCompanion.Dto.csproj", "src/FPLCompanion.Dto/"]
RUN dotnet restore "src/FPLCompanion.Web/FPLCompanion.csproj"
COPY . .
WORKDIR "/src/src/FPLCompanion.Web"
RUN dotnet build "FPLCompanion.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FPLCompanion.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FPLCompanion.dll"]