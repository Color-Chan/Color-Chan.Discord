# Setup base image
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS base

LABEL maintainer="BrammyS <https://github.com/BrammyS>"
LABEL org.label-schema.name="HelloWorldButton"
LABEL org.label-schema.description="A sample project for Color-Chan.Discord"
LABEL org.label-schema.url="https://github.com/Color-Chan/Color-Chan.Discord"

# Setup workspace
COPY . .
WORKDIR /samples/HelloWorldButton

# Restore dependencies
RUN dotnet restore "HelloWorldButton.csproj"  --runtime linux-musl-x64

# Publish the application
RUN dotnet publish "HelloWorldButton.csproj" \
    -c Release -o /app/publish \
    --no-restore \
    --runtime linux-musl-x64 \
    --self-contained true \
    /p:PublishSingleFile=true

# Setup final runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine AS final

# Create a user to run the application
RUN adduser --disabled-password \
  --home /app \
  --gecos '' dotnetuser && chown -R dotnetuser /app

# upgrade musl to remove potential vulnerability
RUN apk upgrade musl

USER dotnetuser
WORKDIR /app
COPY --from=base /app/publish .

ENTRYPOINT ["./HelloWorldButton"]