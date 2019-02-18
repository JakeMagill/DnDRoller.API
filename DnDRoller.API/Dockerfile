FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["DnDRoller.API/DnDRoller.API.csproj", "DnDRoller.API/"]
RUN dotnet restore "DnDRoller.API/DnDRoller.API.csproj"
COPY . .
WORKDIR "/src/DnDRoller.API"
RUN dotnet build "DnDRoller.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DnDRoller.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DnDRoller.API.dll"]