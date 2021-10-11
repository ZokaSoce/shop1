#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Shop1/Shop1.csproj", "Shop1/"]
RUN dotnet restore "Shop1/Shop1.csproj"
COPY . .
WORKDIR "/src/Shop1"
RUN dotnet build "Shop1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop1.dll"]