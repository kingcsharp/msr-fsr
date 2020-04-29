#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY MSR.sln .
COPY MSR.Answer.API/MSR.Answer.API.csproj ./MSR.Answer.API/MSR.Answer.API.csproj
COPY MSR.Infrastructure/MSR.Infrastructure.csproj ./MSR.Infrastructure/MSR.Infrastructure.csproj
COPY MSR.Application/MSR.Application.csproj ./MSR.Application/MSR.Application.csproj
COPY MSR.Domain/MSR.Domain.csproj ./MSR.Domain/MSR.Domain.csproj

RUN dotnet restore "MSR.Answer.API/MSR.Answer.API.csproj"

COPY . .

RUN dotnet build "MSR.Answer.API/MSR.Answer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSR.Answer.API/MSR.Answer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSR.Answer.API.dll"]
