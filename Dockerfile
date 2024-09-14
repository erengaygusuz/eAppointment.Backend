FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./eAppointment.Backend.WebAPI/eAppointment.Backend.WebAPI.csproj", "eAppointment.Backend.WebAPI/"]
COPY ["./eAppointment.Backend.Application/eAppointment.Backend.Application.csproj", "eAppointment.Backend.Application/"]
COPY ["./eAppointment.Backend.Domain/eAppointment.Backend.Domain.csproj", "eAppointment.Backend.Domain/"]
COPY ["./eAppointment.Backend.Infrastructure/eAppointment.Backend.Infrastructure.csproj", "eAppointment.Backend.Infrastructure/"]
RUN dotnet restore "eAppointment.Backend.WebAPI/eAppointment.Backend.WebAPI.csproj"
COPY . .
WORKDIR "/src/eAppointment.Backend.WebAPI"
RUN dotnet build "eAppointment.Backend.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eAppointment.Backend.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN mkdir -p /app/resources

COPY ./eAppointment.Backend.WebAPI/Resources/* /app/resources/

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eAppointment.Backend.WebAPI.dll"]