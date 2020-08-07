FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Web.Service/Web.Service.csproj", "Web.Service/"]
RUN dotnet restore "Web.Service/Web.Service.csproj"
COPY . .
WORKDIR "/src/Web.Service"
RUN dotnet build "Web.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Service.dll"]