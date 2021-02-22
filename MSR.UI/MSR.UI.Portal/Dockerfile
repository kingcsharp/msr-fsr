# Build
FROM 425480257575.dkr.ecr.us-west-2.amazonaws.com/trion-ng-cli AS build

WORKDIR /app
COPY package.json ./
RUN npm install

ARG ENV

COPY . .
RUN npm run $ENV

### STAGE 2: Run ###
FROM 425480257575.dkr.ecr.us-west-2.amazonaws.com/nginx-alpine
COPY --from=build /app/dist/ /usr/share/nginx/html


