FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["DatabaseFirst.csproj", "./"]
RUN dotnet restore "./DatabaseFirst.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DatabaseFirst.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "DatabaseFirst.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DatabaseFirst.dll"]