from mcr.microsoft.com/dotnet/sdk:latest as build

workdir /app

copy *.sln ./
copy PlanetPaintballApi/*.csproj PlanetPaintballApi/
copy PlanetPaintballBL/*.csproj PlanetPaintballBL/
copy PlanetPaintballDL/*.csproj PlanetPaintballDL/
copy PlanetPaintballModel/*.csproj PlanetPaintballModel/
copy PlanetPaintballTest/*.csproj PlanetPaintballTest/

#copy rest of source code from projects
copy . ./

#publish folder with cli command
run dotnet publish -c Release -o publish

#set environment to runtime
from mcr.microsoft.com/dotnet/sdk:latest as runtime

workdir /app
copy --from=build app/publish ./

cmd ["dotnet", "PlanetPaintballApi.dll"]

#expose port 80
expose 80