#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY MSR.Answer.API/MSR.Answer.API.csproj MSR.Answer.API/
COPY MSR.Application/MSR.Application.csproj MSR.Application/
COPY MSR.Domain/MSR.Domain.csproj MSR.Domain/
COPY MSR.Infrastructure/MSR.Infrastructure.csproj MSR.Infrastructure/
RUN dotnet restore "MSR.Answer.API/MSR.Answer.API.csproj"
COPY . .
WORKDIR "/src/MSR.Answer.API"
RUN dotnet build "MSR.Answer.API.csproj" -c Release -o /app/build; exit 0

FROM build AS publish
RUN dotnet publish "MSR.Answer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSR.Answer.API.dll"]
