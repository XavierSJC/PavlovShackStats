FROM node:lts AS builder

ENV NODE_ENV=production

WORKDIR /app
COPY package*.json ./
RUN npm ci

COPY . .

RUN npm run build

FROM nginx:stable-alpine

RUN rm -rf /etc/nginx/conf.d
COPY conf /etc/nginx

COPY --from=builder /app/build /usr/share/nginx/html

EXPOSE 80

WORKDIR /usr/share/nginx/html
COPY ./env.sh .
COPY .env .

# Add bash
RUN apk add --no-cache bash

RUN chmod +x env.sh

CMD ["/bin/bash", "-c", "/usr/share/nginx/html/env.sh && nginx -g \"daemon off;\""]