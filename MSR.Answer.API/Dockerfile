#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY MSR.sln .
COPY MSR.Answer.API/. MSR.Answer.API/
COPY MSR.Application/. MSR.Application/
COPY MSR.Domain/. MSR.Domain/
COPY MSR.Infrastructure/. MSR.Infrastructure/
RUN dotnet restore "MSR.Answer.API/MSR.Answer.API.csproj"
#COPY . .
WORKDIR "/src/MSR.Answer.API"
RUN dotnet build "MSR.Answer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSR.Answer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY --from=gik25138.live.dynatrace.com/linux/oneagent-codemodules-musl:dotnet / /
ENV LD_PRELOAD /opt/dynatrace/oneagent/agent/lib64/liboneagentproc.so
ENV DT_TENANT=gik25138
ENV DT_TENANTTOKEN=dt0c01.JQUTDCF6XMGUMUS6XE5WVCK3.RYRGPWFOLJTCOVNWPLYLKTACMBOLVH7P4MG6HDXEZAXP2YTVFKMN2HIVRGZ2HXYU

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "MSR.Answer.API.dll"]
