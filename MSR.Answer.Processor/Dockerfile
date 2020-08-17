#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY MSR.sln .
COPY MSR.Answer.Processor/. MSR.Answer.Processor/
COPY MSR.Application/. MSR.Application/
COPY MSR.Domain/. MSR.Domain/
COPY MSR.Infrastructure/. MSR.Infrastructure/
RUN dotnet restore "MSR.Answer.Processor/MSR.Answer.Processor.csproj"
#COPY . .
WORKDIR "/src/MSR.Answer.Processor"
RUN dotnet build "MSR.Answer.Processor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSR.Answer.Processor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "MSR.Answer.Processor.dll"]
