
# Bước 1: Dùng image .NET SDK để build dự án
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . . 
RUN dotnet restore
RUN dotnet publish -c Release -o /out

# Bước 2: Dùng image ASP.NET để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
CMD ["dotnet", "DiaryAPI.dll"]
