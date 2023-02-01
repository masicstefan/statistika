FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime 
EXPOSE 81

WORKDIR /app
COPY bin/Debug/net6.0 ./
ENTRYPOINT ["dotnet", "APIStatistika2App.dll"]