FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /build
COPY . ./
RUN dotnet build -c Release --nologo;\
    dotnet publish JuTCo.Web.Host/JuTCo.Web.Host.csproj -c Release --no-build -o /dist/app

FROM node:18-alpine AS frontend-build
WORKDIR /app
COPY jutco.frontend/ .
RUN npm install && npm run build
    

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS app
WORKDIR /app
COPY --from=build-env /dist/app/. .
COPY --from=frontend-build /app/build ./wwwroot
ENV ASPNETCORE_URLS=http://*:8080
CMD ["dotnet", "JuTCo.Web.Host.dll"]