FROM microsoft/dotnet:1.0.0-preview2-sdk
ADD WidgetGallery NuGet.config /WidgetGallery/
WORKDIR /WidgetGallery
RUN dotnet restore && dotnet build
EXPOSE 5555
CMD dotnet run
