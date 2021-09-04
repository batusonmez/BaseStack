#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Api/CommentService/Api.Comment/Api.Comment.csproj", "Api/CommentService/Api.Comment/"]
COPY ["Api/CommentService/Business.Comment/Business.Comment/Business.Comment.csproj", "Api/CommentService/Business.Comment/Business.Comment/"]
RUN dotnet restore "Api/CommentService/Api.Comment/Api.Comment.csproj"
COPY . .
WORKDIR "/src/Api/CommentService/Api.Comment"
RUN dotnet build "Api.Comment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Comment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Comment.dll"]