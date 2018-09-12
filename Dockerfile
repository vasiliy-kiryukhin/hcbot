# Build runtime image
FROM microsoft/aspnetcore
WORKDIR /app
COPY . .
CMD ./HCBot.Runner