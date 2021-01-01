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
        QA_API_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api/397c455c0c042c71"
        QA_UI_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-dev/1b3c1539f365fe8f"
        UAT_API_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api-stage/e7d741c03c9de262"
        UAT_UI_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-ui-stage/3a5df8140101b695"
        PROD_API_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api-prod/0d264f923b3ca5c5"
        PROD_UI_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-ui-prod/a05ebf3f959d039b"
        MESSAGE_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-message-qa/a77318365ce97cd4"
        QA_PROJECT_API='qa-answer-api'
        QA_PROJECT_MESSAGE='qa-answer-message'
        QA_PROJECT_UI='qa-answer-ui'
        UAT_PROJECT_API='uat-answer-api'
        UAT_PROJECT_MESSAGE='uat-answer-message'
        UAT_PROJECT_UI='uat-answer-ui'
        PROD_PROJECT_API='prod-answer-api'
        PROD_PROJECT_MESSAGE='prod-answer-message'
        PROD_PROJECT_UI='prod-answer-ui'
        API_COMPOSE='docker-compose-api.yml'
        API_COMPOSE_PROCESSOR='docker-compose-api-processor.yml'
        API_COMPOSE_MESSAGE='docker-compose-message.yml'
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
                                    //sh "sudo chmod 777 /var/run/docker.sock"
                                    sh "docker build --build-arg ENV=builddevprodsetting -t msr-ui ."
                                    sh "docker tag msr-ui ${ACCOUNT_URL}/msr-ui:${env.GIT_COMMIT}"

                                    sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                    sh "docker push ${ACCOUNT_URL}/msr-ui:${env.GIT_COMMIT}"
                                }
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the UI image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }

                            try {
                                sh "sh update_image.sh QA ${env.GIT_COMMIT} ${UI_COMPOSE}"
                                sh "cat ${UI_COMPOSE}"

                                //withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                    //sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
                                    //sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

                                    if(env.BRANCH_NAME == 'Develop') {
                                        echo "Deploying Develop"
                                        //deploy("${UI_COMPOSE}", "${QA_PROJECT_UI}", "${QA_UI_TARGET_ARN}", "app")
                                    }
                                //}

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
                                    echo "Building api proxy container...."
                                    sh "docker build --build-arg NGINX_CONF=dev -t msr-rp ."
                                    sh "docker tag msr-rp ${ACCOUNT_URL}/msr-rp:${env.GIT_COMMIT}"

                                    sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                    sh "docker push ${ACCOUNT_URL}/msr-rp:${env.GIT_COMMIT}"
                                }

                                dir('messageproxy') {
                                    echo "Building message proxy container...."

                                    sh "docker build --build-arg NGINX_CONF=dev -t msr-mp ."
                                    sh "docker tag msr-mp ${ACCOUNT_URL}/msr-mp:${env.GIT_COMMIT}"

                                    sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                    sh "docker push ${ACCOUNT_URL}/msr-mp:${env.GIT_COMMIT}"
                                }
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the NGINX image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }

                            try {
                                echo "Building API container...."
                                sh "docker build -f MSR.Answer.API/Dockerfile -t msr-api ."
                                sh "docker tag msr-api ${ACCOUNT_URL}/msr-api:${env.GIT_COMMIT}"

                                sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                sh "docker push ${ACCOUNT_URL}/msr-api:${env.GIT_COMMIT}"

                                echo "Building Processor container...."
                                sh "docker build -f MSR.Answer.Processor/Dockerfile -t msr-processor ."
                                sh "docker tag msr-processor ${ACCOUNT_URL}/msr-processor:${env.GIT_COMMIT}"

                                sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                sh "docker push ${ACCOUNT_URL}/msr-processor:${env.GIT_COMMIT}"

                                echo "Building Message container...."
                                sh "docker build -f MSR.Answer.MessageHub/Dockerfile -t msr-message ."
                                sh "docker tag msr-message ${ACCOUNT_URL}/msr-message:${env.GIT_COMMIT}"

                                sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                sh "docker push ${ACCOUNT_URL}/msr-message:${env.GIT_COMMIT}"
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the API image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }

                            try {
                                sh "sh update_image_api.sh QA ${env.GIT_COMMIT} ${API_COMPOSE}"
                                sh "sh update_image_api.sh QA ${env.GIT_COMMIT} ${API_COMPOSE_PROCESSOR}"
                                sh "sh update_image_api.sh QA ${env.GIT_COMMIT} ${API_COMPOSE_MESSAGE}"
                                sh "cat ${API_COMPOSE}"
                                sh "cat ${API_COMPOSE_PROCESSOR}"
                                sh "cat ${API_COMPOSE_MESSAGE}"

                                //withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                    //sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
                                    //sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

                                echo "Deploying Develop"
                                //deploy("${API_COMPOSE}", "${QA_PROJECT_API}", "${QA_API_TARGET_ARN}", "reverseproxy")
                                //deploy_processor("${API_COMPOSE_PROCESSOR}", "${QA_PROJECT_API}", "${QA_API_TARGET_ARN}", "processor")
                                deploy("${API_COMPOSE_MESSAGE}", "${QA_PROJECT_MESSAGE}", "${MESSAGE_TARGET_ARN}", "message")

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

        stage("Deploy Rollbar QA") {
            agent { label 'master' }
            steps {
                script {
                    sh "curl https://api.rollbar.com/api/1/deploy/ \\\n" +
                            "  -F access_token=145adf4dbb224fd6b94382baf8c00ec3 \\\n" +
                            "  -F environment=QA \\\n" +
                            "  -F revision=\"${env.GIT_COMMIT}\" \\\n" +
                            "  -F local_username=system"
                }
            }
        }

        stage('Promoting to UAT?') {
            agent { label 'master' }
            steps {
                script {
                    timeout(activity: true, time: 5) {
                        input message: 'Are you ready to deploy to UAT?', parameters: [booleanParam(defaultValue: true, description: '', name: '')]
                    }
                }
            }
        }

        stage('Promoting to UAT') {
            parallel {
                stage("Promoting API to UAT") {
                    agent { label 'master' }
                    steps {
                        script {
                            sh "sh update_image_api.sh Stage ${env.GIT_COMMIT} ${API_COMPOSE}"
                            sh "sh update_image_api.sh Stage ${env.GIT_COMMIT} ${API_COMPOSE_PROCESSOR}"
                            sh "cat ${API_COMPOSE}"
                            sh "cat ${API_COMPOSE_PROCESSOR}"

                            deploy("${API_COMPOSE}", "${UAT_PROJECT_API}", "${UAT_API_TARGET_ARN}", "reverseproxy")
                            deploy_processor("${API_COMPOSE_PROCESSOR}", "${UAT_PROJECT_API}", "${UAT_API_TARGET_ARN}", "processor")
                        }
                    }
                }

                stage("Promote UI to UAT") {
                    agent { label 'master' }
                    steps {
                        script {
                            dir('MSR.UI/MSR.UI.Answer') {
                                //sh "sudo chmod 777 /var/run/docker.sock"
                                sh "docker build --build-arg ENV=buildstageprodsetting -t msr-ui ."
                                sh "docker tag msr-ui ${ACCOUNT_URL}/msr-ui:${env.GIT_COMMIT}"

                                sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                sh "docker push ${ACCOUNT_URL}/msr-ui:${env.GIT_COMMIT}"
                            }

                            sh "sh update_image.sh Stage ${env.GIT_COMMIT} ${UI_COMPOSE}"
                            sh "cat ${UI_COMPOSE}"

                            deploy("${UI_COMPOSE}", "${UAT_PROJECT_UI}", "${UAT_UI_TARGET_ARN}", "app")
                            office365ConnectorSend color: "${GREEN}", message: "${env.BRANCH_NAME} UI was promoted successfully.", status: 'Passed', webhookUrl: "${WEBHOOK_URL}"
                        }
                    }
                }
            }
        }

        stage("Deploy Rollbar UAT") {
            agent { label 'master' }
            steps {
                script {
                    sh "curl https://api.rollbar.com/api/1/deploy/ \\\n" +
                            "  -F access_token=145adf4dbb224fd6b94382baf8c00ec3 \\\n" +
                            "  -F environment=UAT \\\n" +
                            "  -F revision=\"${env.GIT_COMMIT}\" \\\n" +
                            "  -F local_username=system"
                }
            }
        }

        stage('Promoting to Production?') {
            agent { label 'master' }
            steps {
                script {
                    timeout(activity: true, time: 5) {
                        input message: 'Are you ready to deploy to Production?', parameters: [booleanParam(defaultValue: true, description: '', name: '')]
                    }
                }
            }
        }

        stage('Promoting to Production') {
            parallel {
                stage("Promote API to PROD") {
                    agent { label 'master' }
                    steps {
                        script {
                            sh "sh update_image_api.sh Production ${env.GIT_COMMIT} ${API_COMPOSE}"
                            sh "sh update_image_api.sh Production ${env.GIT_COMMIT} ${API_COMPOSE_PROCESSOR}"

                            sh "cat ${API_COMPOSE}"
                            sh "cat ${API_COMPOSE_PROCESSOR}"
                            //deploy("${API_COMPOSE}", "${PROD_PROJECT_API}", "${PROD_API_TARGET_ARN}", "reverseproxy")
                            sh "ecs-cli compose --file docker-compose-api.yml --ecs-params ecs-params-api.yml --project-name prod-answer-api service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn ${PROD_API_TARGET_ARN} --container-name reverseproxy --container-port 80 --timeout 15"

                            deploy_processor("${API_COMPOSE_PROCESSOR}", "${PROD_PROJECT_API}", "${PROD_API_TARGET_ARN}", "processor")
                        }
                    }
                }

                stage("Promote UI to PROD") {
                    agent { label 'master' }
                    steps {
                        script {
                            dir('MSR.UI/MSR.UI.Answer') {
                                sh "docker build --build-arg ENV=buildprod -t msr-ui ."
                                sh "docker tag msr-ui ${ACCOUNT_URL}/msr-ui:${env.GIT_COMMIT}"

                                sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                sh "docker push ${ACCOUNT_URL}/msr-ui:${env.GIT_COMMIT}"
                            }

                            sh "sh update_image.sh Production ${env.GIT_COMMIT} ${UI_COMPOSE}"
                            sh "cat ${UI_COMPOSE}"

                            deploy("${UI_COMPOSE}", "${PROD_PROJECT_UI}", "${PROD_UI_TARGET_ARN}", "app")
                            office365ConnectorSend color: "${GREEN}", message: "${env.BRANCH_NAME} UI was promoted successfully.", status: 'Passed', webhookUrl: "${WEBHOOK_URL}"
                        }
                    }
                }
            }
        }

        stage("Deploy Rollbar PROD") {
            agent { label 'master' }
            steps {
                script {
                    sh "curl https://api.rollbar.com/api/1/deploy/ \\\n" +
                            "  -F access_token=145adf4dbb224fd6b94382baf8c00ec3 \\\n" +
                            "  -F environment=production \\\n" +
                            "  -F revision=\"${env.GIT_COMMIT}\" \\\n" +
                            "  -F local_username=system"
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
    withCredentials([usernamePassword(credentialsId: 'aws-msrfsr-key-secret', passwordVariable: 'PASS', usernameVariable: 'KEY')]) {
        sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
        sh "ecs-cli configure profile --access-key ${KEY} --secret-key ${PASS} --profile-name answer-profile"

        sh "ecs-cli compose --file ${composeFile} --project-name ${name} service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn ${target} --container-name ${app} --container-port 80 --timeout 15"
        //sh "ecs-cli compose --file ${composeFile} --project-name ${name} --cluster-config answer-config --ecs-profile answer-profile service scale 2"
    }
}

void deploy_processor(composeFile,name,target, app) {
    withCredentials([usernamePassword(credentialsId: 'aws-msrfsr-key-secret', passwordVariable: 'PASS', usernameVariable: 'KEY')]) {
        sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
        sh "ecs-cli configure profile --access-key ${KEY} --secret-key ${PASS} --profile-name answer-profile"

        sh "ecs-cli compose --file ${composeFile} --project-name ${name}-processor service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --timeout 15"
    }
}