#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
#EXPOSE 80

#restore and build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ExploreCalifornia.csproj", ""]
RUN dotnet restore "./ExploreCalifornia.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ExploreCalifornia.csproj" -c Release -o /app/build

# testing
FROM build AS testing
WORKDIR /src
RUN dotnet test

#publush
FROM build AS publish
RUN dotnet publish "ExploreCalifornia.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ExploreCalifornia.dll"]

# heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ExploreCalifornia.dll