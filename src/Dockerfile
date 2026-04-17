FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

WORKDIR /app

EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["HelloWorld.Main/HelloWorld.Main.csproj", "HelloWorld.Main/"]
COPY ["HelloWorld.Service/HelloWorld.Service.csproj", "HelloWorld.Service/"]
COPY ["HelloWorld.Application/HelloWorld.Application.csproj", "HelloWorld.Application/"]
COPY ["HelloWorld.DataAccess/HelloWorld.DataAccess.csproj", "HelloWorld.DataAccess/"]
COPY ["HelloWorld.Models/HelloWorld.Models.csproj", "HelloWorld.Models/"]

RUN dotnet restore "HelloWorld.Main/HelloWorld.Main.csproj"

COPY . .

WORKDIR "/src/HelloWorld.Main"

RUN dotnet publish -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "HelloWorld.Main.dll"]
