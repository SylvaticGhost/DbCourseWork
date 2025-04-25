FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DbCourseWork.App/DbCourseWork.App.csproj", "src/DbCourseWork.App/"]
COPY ["src/DbCourseWork.Core/DbCourseWork.Core.csproj", "src/DbCourseWork.Core/"]
COPY ["src/DbCourseWork.Data/DbCourseWork.Data.csproj", "src/DbCourseWork.Data/"]
COPY ["src/DbCourseWork.Services/DbCourseWork.Services.csproj", "src/DbCourseWork.Services/"]
COPY ["src/DbCourseWork.Utils/DbCourseWork.Utils.csproj", "src/DbCourseWork.Utils/"]

RUN dotnet restore "src/DbCourseWork.App/DbCourseWork.App.csproj"
COPY . .
WORKDIR "/src/src/DbCourseWork.App"
RUN dotnet build "DbCourseWork.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DbCourseWork.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DbCourseWork.App.dll"]