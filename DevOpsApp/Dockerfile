FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

ARG SONAR_PROJECT_KEY=devopsdockerdotnetapp
ARG SONAR_OGRANIZAION_KEY=devopsdockerdotnetapp
ARG SONAR_HOST_URL=https://sonarcloud.io
ARG SONAR_TOKEN=30d331de2733c62c306c1f56308babfdc66e1656

RUN apt-get update && apt-get install -y openjdk-11-jdk
RUN dotnet tool install --global dotnet-sonarscanner --version 5.3.2
RUN dotnet tool install --global coverlet.console
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet sonarscanner begin /o:"$SONAR_OGRANIZAION_KEY" /k:"$SONAR_PROJECT_KEY" /d:sonar.host.url="$SONAR_HOST_URL" /d:sonar.login="$SONAR_TOKEN"
COPY *.csproj .
COPY . .

RUN dotnet publish -o /out/
RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput="/coverage"
RUN dotnet sonarscanner end /d:sonar.login="$SONAR_TOKEN"

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS run
WORKDIR /app
EXPOSE 5000
COPY --from=build /out .
ENTRYPOINT [ "dotnet", "DevOpsApp.dll"]
