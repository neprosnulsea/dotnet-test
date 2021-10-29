FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
 WORKDIR /app
 EXPOSE 80
 FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
 WORKDIR /src
 COPY [".", ""]
 RUN dotnet restore "./DevOpsApp/DevOpsApp/DevOpsApp.csproj"
 COPY . .
 WORKDIR "/src/."
 RUN dotnet build "DevOpsApp/DevOpsApp/DevOpsApp.csproj" -c Release -o /app/build
 FROM build AS publish
 RUN dotnet publish "DevOpsApp/DevOpsApp/DevOpsApp.csproj" -c Release -o /app/publish
 FROM base AS final
 WORKDIR /app
 COPY --from=publish /app/publish .
 ENTRYPOINT ["dotnet", "DevOpsApp.dll"]

 #unit

 # FROM sonarqube:latest as sonar
 #sonarcloud account plugins(depends)