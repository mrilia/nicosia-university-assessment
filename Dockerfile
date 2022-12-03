FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./Nicosia.Assessment.WebApi/Nicosia.Assessment.WebApi.csproj ./Nicosia.Assessment.WebApi/
COPY ./Nicosia.Assessment.Shared/Nicosia.Assessment.Shared.csproj ./Nicosia.Assessment.Shared/
COPY ./Nicosia.Assessment.WebUI/Nicosia.Assessment.WebUI.csproj ./Nicosia.Assessment.WebUI/
COPY ./Nicosia.Assessment.Application/Nicosia.Assessment.Application.csproj ./Nicosia.Assessment.Application/
COPY ./Nicosia.Assessment.Domain/Nicosia.Assessment.Domain.csproj ./Nicosia.Assessment.Domain/
COPY ./Nicosia.Assessment.Persistence/Nicosia.Assessment.Persistence.csproj ./Nicosia.Assessment.Persistence/
COPY ./Nicosia.Assessment.AcceptanceTests/Nicosia.Assessment.AcceptanceTests.csproj ./Nicosia.Assessment.AcceptanceTests/
COPY ./Nicosia.Assessment.UnitTests/Nicosia.Assessment.UnitTests.csproj ./Nicosia.Assessment.UnitTests/
COPY *.sln .
RUN dotnet restore
COPY . .


FROM build AS publish
RUN dotnet publish -c Release ./Nicosia.Assessment.WebApi/Nicosia.Assessment.WebApi.csproj -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nicosia.Assessment.WebApi.dll"]



