#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MessageService/MessageService.csproj", "src/MessageService/"]
RUN dotnet restore "src/MessageService/MessageService.csproj"
COPY . .
WORKDIR "/src/src/MessageService"
RUN dotnet build "MessageService.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "MessageService.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageService.dll"]