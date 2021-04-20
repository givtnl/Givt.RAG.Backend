FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN apt-get update \
    && apt-get --yes install libgdiplus

RUN mkdir -p /home/site/wwwroot

ENV ASPNETCORE_URLS=http://*:80

COPY publish/ /home/site/wwwroot/

WORKDIR /home/site/wwwroot/

ENTRYPOINT [ "dotnet", "backend.dll" ]