![Assertible status](https://assertible.com/apis/7c13748e-0e5d-43e5-9d74-6f4e7f09bb0a/status?api_token=gtgNgcGA8j38t2TH)
<br/>
(https://assertible.com/dashboard#/services/7c13748e-0e5d-43e5-9d74-6f4e7f09bb0a/results)
# Running the app manually #

## The following needs to be installed to run Answer3.0 ##

- Visual Studio 2019
    -  [Windows Version](https://visualstudio.microsoft.com/downloads/)
        - Modules Required
            - ASP.NET and web development
            - .Net Core cross-platform development 
    -  [Mac Version](https://visualstudio.microsoft.com/vs/mac/)
        - **Note:** Since Database projects are not supported on Visual Studio for Mac, if you are doing backend development it might be best to use a Windows 10 Pro Environment
- [Visual Studio Code](https://code.visualstudio.com/download)
    - **Note:** Install Debugger for Chrome if you would like to use the Visual Studio Code debugger
    - [Debugger for Chrome](https://github.com/Microsoft/vscode-chrome-debug)
- [NodeJs with NPM](https://nodejs.org/en/download/)
- [Yarn](https://classic.yarnpkg.com/en/docs/install)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)
- A Dedicated IP
    - Note: If you do not have a static IP, you can use any Dedicated IP Services. Here are two popular ones used. Make sure you contact the helpdesk to gain access to the database by submitting your dedicated IP Address [Help Desk](support-mb@cmhworks.com))
    - [PureVPN](https://www.purevpn.com)
    - [NordVPN](https://nordvpn.com/)

## The following commands work for Windows 10 ##

**Note:** Refer to the README located in **\MSR.UI\MSR.UI.Answer** for more information about the UI and running UI tests

**API:**
    To run the backend, open the solution in Visual Studio 2019 and run it with MSR.Answer.API as your startup project

**UI:** 
Beware that the UI will not start if the backend isn't running or
otherwise cannot be connected to.  Usually running it first in visual
studio will suffice.

    cd MSR.UI/MSR.UI.Answer

    yarn install

    npm run start

UI is running on port 3000

## The following commands work for MacOS ##

**Note:** When openning the solution in Visual Studio 2019, the Database and Angular project will not load. The backend will still build and run properly, but this is due to VS for Mac not supporting Database Projects and the Angular project not having a **.csproj** file.

**API:** 

    dotnet run -p MSR.Answer.API/MSR.Answer.API.csproj --launch-profile local

API is running on port 5000 (http) & 5001 (https)

**UI:** 

    cd MSR.UI/MSR.UI.Answer && yarn install && npm run start 

UI is running on port 3000

<hr />

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

