#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Api/BookService/Api.Book/Api.BookManagement.csproj", "Api/BookService/Api.Book/"]
COPY ["Api/BookService/Business.Book/Business.Book/Business.Book.csproj", "Api/BookService/Business.Book/Business.Book/"]
COPY ["Api/BookService/BookManagementModels/BookManagementModels/BookManagementModels.csproj", "Api/BookService/BookManagementModels/BookManagementModels/"]
COPY ["Core/Data/Indexer/Indexer/Indexer.csproj", "Core/Data/Indexer/Indexer/"]
COPY ["Core/Data/UnitOfWork/UnitOfWork/UnitOfWork.csproj", "Core/Data/UnitOfWork/UnitOfWork/"]
COPY ["Core/Data/Repository/Repository.csproj", "Core/Data/Repository/"]
RUN dotnet restore "Api/BookService/Api.Book/Api.BookManagement.csproj"
COPY . .
WORKDIR "/src/Api/BookService/Api.Book"
RUN dotnet build "Api.BookManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.BookManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.BookManagement.dll"]