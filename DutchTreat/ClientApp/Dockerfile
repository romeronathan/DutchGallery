#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:10.15-alpine AS client 
ARG skip_client_build=false 
WORKDIR /app 
COPY ./DutchTreat/ClientApp . 

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./DutchTreat.csproj", "DutchTreat/"]
RUN dotnet restore "DutchTreat/DutchTreat.csproj"
COPY . .
WORKDIR "/src/DutchTreat"
RUN dotnet build "DutchTreat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DutchTreat.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DutchTreat.dll"]