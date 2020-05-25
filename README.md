<a href='https://jenkins.cmhworks.com/job/MSR-FSR/job/Answer3/job/Develop/'><img src='https://jenkins.cmhworks.com/buildStatus/icon?job=MSR-FSR%2FAnswer3%2FDevelop'></a>

## Running the app manually

**API:** `dotnet run -p MSR.Answer.API/MSR.Answer.API.csproj --launch-profile web`
<br/>
API is running on port 5000 (http) & 5001 (https)

**UI:** `cd MSR.UI/MSR.UI.Answer && npm install && npm run start` 
<br/>
UI is running on port 3000

## Running with Docker
**Docker:** `docker-compose up --build`

* UI is running on port 3000
* API is running on port 5000

**Bring down Docker:** `docker-compose down`

## Building Images

**API:** `docker build -f MSR.Answer.API/Dockerfile -t msr-api .`
<br/>
**UI:** `cd MSR.UI/MSR.UI.Answer && docker build -t msr-ui .`
<br/>
**NGINX:** `cd reverseproxy && docker build -t msr-rp .`

## Build and push to ECS

#### UI
1. `aws ecr get-login-password --region us-west-2 | docker login --username AWS --password-stdin 425480257575.dkr.ecr.us-west-2.amazonaws.com`
2. `docker build -t msr-ui .`
3. `docker tag msr-ui:latest 425480257575.dkr.ecr.us-west-2.amazonaws.com/msr-ui:latest`
4. `docker push 425480257575.dkr.ecr.us-west-2.amazonaws.com/msr-ui:latest`

#### API
1. `aws ecr get-login-password --region us-west-2 | docker login --username AWS --password-stdin 425480257575.dkr.ecr.us-west-2.amazonaws.com`
2. `docker build -t msr-api .`
3. `docker tag msr-api:latest 425480257575.dkr.ecr.us-west-2.amazonaws.com/msr-api:latest`
4. `docker push 425480257575.dkr.ecr.us-west-2.amazonaws.com/msr-api:latest`

#### NGINX
1. `aws ecr get-login-password --region us-west-2 | docker login --username AWS --password-stdin 425480257575.dkr.ecr.us-west-2.amazonaws.com`
2. `docker build -t msr-rp .`
3. `docker tag msr-rp:latest 425480257575.dkr.ecr.us-west-2.amazonaws.com/msr-rp:latest`
4. `docker push 425480257575.dkr.ecr.us-west-2.amazonaws.com/msr-rp:latest`

## Deploy to AWS ECS Fargate
#### UI 
**Map cluster:** `ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2`
<br/>
**Configure profile (You should have your AWS CLI setup locally) :** `ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile`
<br/>
**Deploy to cluster:** `ecs-cli compose --file docker-compose-ui.yml --project-name answer-ui service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-dev/1b3c1539f365fe8f --container-name app --container-port 80 --timeout 10`

#### API & NGINX

