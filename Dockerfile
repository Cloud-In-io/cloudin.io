FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

RUN dotnet publish ./Core/WebApi -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build /app .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "WebApi.dll"]