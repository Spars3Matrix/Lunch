FROM microsoft/dotnet:2.1-sdk
WORKDIR /lunch/Lunch
EXPOSE 5000

ENV DOTNET_USE_POLLING_FILE_WATCHER 1
ENV ASPNETCORE_ENVIRONMENT Development

RUN apt-get update
RUN apt-get install -y unzip procps
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l ~/vsdbg

COPY ["Lunch.csproj", "./"]
COPY ["Directory.Build.props", "./"]
RUN dotnet restore "./Lunch.csproj"

ENTRYPOINT [ "dotnet", "watch", "run", "--urls", "https://0.0.0.0:5000" ]