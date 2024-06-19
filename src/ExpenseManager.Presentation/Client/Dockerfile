FROM node:18-alpine AS base
WORKDIR /app

FROM base AS prod
COPY . .
RUN npm install && npm run build
EXPOSE 4173
CMD ["npm", "run", "preview"]