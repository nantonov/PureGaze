FROM mcr.microsoft.com/dotnet/sdk:10.0
RUN dotnet dev-certs https
WORKDIR /app
EXPOSE 7254
ENTRYPOINT dotnet run --project ./PureGaze.API/PureGaze.API.csproj --no-launch-profile