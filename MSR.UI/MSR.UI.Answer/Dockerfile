# Build
FROM trion/ng-cli AS build

WORKDIR /app
COPY package.json ./
COPY yarn.lock ./
RUN node --version
RUN yarn install

ARG ENV

COPY . .
RUN npm run $ENV

### STAGE 2: Run ###
FROM nginx:1.17.1-alpine
COPY --from=build /app/dist/ /usr/share/nginx/html


