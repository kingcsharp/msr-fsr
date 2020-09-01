pipeline {
    options {
        disableConcurrentBuilds()
    }
    agent none
    environment {
        WEBHOOK_URL = 'https://outlook.office.com/webhook/19c3ea6c-d421-4b34-bf8b-9188e9e5c729@f139f56d-9238-4269-8e2e-8b0f314429cb/JenkinsCI/a8556c572acf47fdaa48888079cd22f5/73b18003-3808-48a4-bf4d-a0fc2650baa5'
        GREEN = '#008000'
        RED = '#FF0000'
        ACCOUNT_URL='425480257575.dkr.ecr.us-west-2.amazonaws.com'
        REGION='us-west-2'
        PROFILE='--profile msrfsr'
        DEV_API_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api/397c455c0c042c71"
        DEV_UI_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-dev/1b3c1539f365fe8f"
        STAGE_API_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api-stage/e7d741c03c9de262"
        STAGE_UI_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-ui-stage/3a5df8140101b695"
        DEV_PROJECT_API='dev-answer-api'
        DEV_PROJECT_UI='dev-answer-ui'
        STAGE_PROJECT_API='stage-answer-api'
        STAGE_PROJECT_UI='stage-answer-ui'
        API_COMPOSE='docker-compose-api.yml'
        UI_COMPOSE='docker-compose-ui.yml'
    }
    stages {
        //stage("Running xUnit Tests") {
        //    agent { label 'ubuntu-node' }
        //    steps {
        //        script {
        //            catchError(buildResult: 'SUCCESS', stageResult: 'FAILURE') {
        //                sh "git mv Msr.Infrastructure MSR.Infrastructure"
        //                sh 'dotnet restore "MSR.Answer.API/MSR.Answer.API.csproj"'
        //                sh 'dotnet test MSR.Application.Tests/ --logger trx;LogFileName=unit_tests.xml'
        //                sh 'dotnet test MSR.Domain.Tests/ --logger trx;LogFileName=unit_tests.xml'
        //                sh 'dotnet test MSR.Infrastructure.Tests/ --logger trx;LogFileName=unit_tests.xml'
        //                step([$class: 'MSTestPublisher', testResultsFile: "**/*.trx", failOnError: true, keepLongStdio: true])
        //                sh "exit 1"
        //            }
        //        }
        //    }
        //}
        stage('Build & Deploy') {
            parallel {
                stage('Build & Deploy UI to QA') {
                    agent { label 'master'}
                    steps {
                        script {
                            try {
                                dir('MSR.UI/MSR.UI.Answer') {
                                    sh "sudo chmod 777 /var/run/docker.sock"
                                    sh "docker build --build-arg ENV=dev -t msr-ui ."
                                    sh "docker tag msr-ui ${ACCOUNT_URL}/msr-ui:${env.GIT_COMMIT}"

                                    sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                    sh "docker push ${ACCOUNT_URL}/msr-ui:${env.GIT_COMMIT}"
                                }
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the UI image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }

                            try {
                                sh "sudo sh update_image.sh ${env.BRANCH_NAME} ${env.GIT_COMMIT} ${UI_COMPOSE}"
                                sh "cat ${UI_COMPOSE}"

                                withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                    sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
                                    sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

                                    if(env.BRANCH_NAME == 'Develop') {
                                        echo "Deploying Develop"
                                        deploy("${UI_COMPOSE}", "${DEV_PROJECT_UI}", "${DEV_UI_TARGET_ARN}", "app")
                                    }
                                }

                                office365ConnectorSend color: "${GREEN}", message: "${env.BRANCH_NAME} UI deployed successfully.", status: 'Passed',webhookUrl: "${WEBHOOK_URL}"

                            } catch (e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED deploying the UI containers. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }
                        }
                    }
                }
                stage('Build and Deploy API to QA') {
                    agent { label 'master'}
                    steps {
                        script {
                            try {
                                dir('reverseproxy') {
                                    sh "sudo chmod 777 /var/run/docker.sock"
                                    sh "docker build --build-arg NGINX_CONF=dev -t msr-rp ."
                                    sh "docker tag msr-rp ${ACCOUNT_URL}/msr-rp:${env.GIT_COMMIT}"

                                    sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                    sh "docker push ${ACCOUNT_URL}/msr-rp:${env.GIT_COMMIT}"
                                }
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the NGINX image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }

                            try {
                                sh "sudo chmod 777 /var/run/docker.sock"
                                //sh "git mv Msr.Infrastructure MSR.Infrastructure"
                                sh "docker build -f MSR.Answer.API/Dockerfile -t msr-api ."
                                sh "docker tag msr-api ${ACCOUNT_URL}/msr-api:${env.GIT_COMMIT}"

                                sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                sh "docker push ${ACCOUNT_URL}/msr-api:${env.GIT_COMMIT}"
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the API image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }

                            try {
                                sh "sudo sh update_image_api.sh Development ${env.GIT_COMMIT} ${API_COMPOSE}"
                                sh "cat ${API_COMPOSE}"

                                withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                    sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
                                    sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

                                    if(env.BRANCH_NAME == 'Develop') {
                                        echo "Deploying Develop"
                                        deploy("${API_COMPOSE}", "${DEV_PROJECT_API}", "${DEV_API_TARGET_ARN}", "reverseproxy")
                                    }
                                }

                                office365ConnectorSend color: "${GREEN}", message: "${env.BRANCH_NAME} API deployed successfully.", status: 'Passed',webhookUrl: "${WEBHOOK_URL}"

                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED deploying the API. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }
                        }
                    }
                }
            }
        }

        stage("Promote UI & API to UAT") {
            steps {
                script {
                    timeout(activity: true, time: 5) {
                        input message: 'Are you ready to deploy to UAT?', parameters: [booleanParam(defaultValue: false, description: '', name: 'choice')]
                    }
                    deploy("${API_COMPOSE}", "${STAGE_PROJECT_API}", "${STAGE_API_TARGET_ARN}", "reverseproxy")
                    deploy("${UI_COMPOSE}", "${STAGE_PROJECT_UI}", "${STAGE_UI_TARGET_ARN}", "app")
                }
            }
        }

        stage("Promote UI & API to PRODUCTION") {
            steps {
                script {
                    timeout(activity: true, time: 5) {
                        input message: 'Are you ready to deploy to PRODUCTION?', parameters: [booleanParam(defaultValue: false, description: '', name: 'choice')]
                    }
                    deploy("${API_COMPOSE}", "${STAGE_PROJECT_API}", "${STAGE_API_TARGET_ARN}", "reverseproxy")
                    deploy("${UI_COMPOSE}", "${STAGE_PROJECT_UI}", "${STAGE_UI_TARGET_ARN}", "app")
                }
            }
        }

        /*
        stage("Running API Tests") {
            agent { label 'jenkins-ecs-slave' }
            steps {
                script {
                    sh label: '', script: '''curl -u Vi5GHlZj0Cb5sUlC: "https://assertible.com/deployments" -d\'{
                        "service": "7c13748e-0e5d-43e5-9d74-6f4e7f09bb0a",
                        "environment": "dev",
                        "version": "v1",
                        "ref": "'"$(git rev-parse HEAD)"'",
                        "github": true
                    }\''''
                }
            }
        }
        */
        /*
        stage("Run Cypress Test") {
            agent { label 'master' }
            steps {
                script {
                    sh "sudo chmod 777 /var/run/docker.sock"
                    sh "git mv Msr.Infrastructure MSR.Infrastructure"
                    sh 'docker-compose up --build -d'
                    sh './count_containers.sh running'
                    dir('MSR.UI/MSR.UI.Answer') {
                        sh 'yarn'
                        sh './node_modules/.bin/cypress run --record --key 48818e2d-4f0f-4541-8176-b9541ee0064d'
                    }
                    sh 'ocker-compose down'
                }
            }
        }
        */
    }
}

void deploy(composeFile,name,target, app) {
    withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
        sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
        sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

        sh "ecs-cli compose --file ${composeFile} --project-name ${name} service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn ${target} --container-name ${app} --container-port 80 --timeout 15"
    }
}