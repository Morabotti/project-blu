#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProjectBlu/ProjectBlu.csproj", "ProjectBlu/"]
RUN dotnet restore "ProjectBlu/ProjectBlu.csproj"
COPY . .
WORKDIR "/src/ProjectBlu"
RUN dotnet build "ProjectBlu.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectBlu.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectBlu.dll"]