FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /tasks.backend.initservice
COPY *.csproj ./
RUN dotnet restore

COPY ./ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster
#RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
#&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
#&& wget https://download.newrelic.com/548C16BF.gpg \
#&& apt-key add 548C16BF.gpg \
#&& apt-get update \
#&& apt-get install -y newrelic-netcore20-agent

#ENV CORECLR_ENABLE_PROFILING=1 \
#CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
#CORECLR_NEWRELIC_HOME=/usr/local/newrelic-netcore20-agent \
#CORECLR_PROFILER_PATH=/usr/local/newrelic-netcore20-agent/libNewRelicProfiler.so \
#NEW_RELIC_LICENSE_KEY=42a1384e0d1f0e20ee28ccd24f85ca3a5aac3c58 \
#NEW_RELIC_APP_NAME=initservice

WORKDIR /tasks.backend.initservice
COPY --from=build /tasks.backend.initservice/out .
ENV ASPNETCORE_URLS http://*:52101
ENV ASPNETCORE_ENVIRONMENT Docker
#ENV RedisConnection 192.168.99.101:30379
ENTRYPOINT ["dotnet", "tasks.backend.initservice.dll"]