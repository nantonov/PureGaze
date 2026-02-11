FROM node:24-bookworm
WORKDIR /app
EXPOSE 5173
ENTRYPOINT npm install && npm run dev_container