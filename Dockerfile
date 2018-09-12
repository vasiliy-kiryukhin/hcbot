# Build runtime image
FROM microsoft/aspnetcore
WORKDIR /app
COPY . .
VOLUME /usr/local/hcbot-data
CMD ./HCBot.Runner /usr/local/hcbot-data