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
    }
    stages {
        stage('Build & Deploy') {
            parallel {
                stage('Build and Deploy UI') {
                    agent { label 'master'}
                    steps {
                        script {
                            try {
                                dir('MSR.UI/MSR.UI.Answer') {
                                    sh "sudo chmod 777 /var/run/docker.sock"
                                    sh "docker build -f prod.Dockerfile -t msr-ui ."
                                    sh "docker tag msr-ui ${ACCOUNT_URL}/msr-ui:${env.BRANCH_NAME}${env.BUILD_NUMBER}"

                                    sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                    sh "docker push ${ACCOUNT_URL}/msr-ui:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                                }
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the UI image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }

                            try {
                                sh "sudo sh update_image.sh ${env.BRANCH_NAME} ${env.BUILD_NUMBER} docker-compose-ui.yml"
                                sh "cat docker-compose-ui.yml"

                                withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                    sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
                                    sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

                                    sh "ecs-cli compose --file docker-compose-ui.yml --project-name answer-ui service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-dev/1b3c1539f365fe8f --container-name app --container-port 80 --timeout 15"
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

                stage('Build and Deploy API') {
                    agent { label 'master'}
                    steps {
                        script {
                            try {
                                dir('reverseproxy') {
                                    sh "sudo chmod 777 /var/run/docker.sock"
                                    sh "docker build --build-arg NGINX_CONF=dev -t msr-rp ."
                                    sh "docker tag msr-rp ${ACCOUNT_URL}/msr-rp:${env.BRANCH_NAME}${env.BUILD_NUMBER}"

                                    sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                    sh "docker push ${ACCOUNT_URL}/msr-rp:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                                }
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the NGINX image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }

                            try {
                                sh "sudo chmod 777 /var/run/docker.sock"
                                sh "git mv Msr.Infrastructure MSR.Infrastructure"
                                sh "docker build -f MSR.Answer.API/Dockerfile -t msr-api ."
                                sh "docker tag msr-api ${ACCOUNT_URL}/msr-api:${env.BRANCH_NAME}${env.BUILD_NUMBER}"

                                sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                sh "docker push ${ACCOUNT_URL}/msr-api:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the API image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }

                            try {
                                sh "sudo sh update_image_api.sh ${env.BRANCH_NAME} ${env.BUILD_NUMBER} docker-compose-api.yml"
                                sh "cat docker-compose-api.yml"

                                withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                    sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
                                    sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

                                    sh "ecs-cli compose --file docker-compose-api.yml --project-name answer-api service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api/397c455c0c042c71 --container-name reverseproxy --container-port 80 --timeout 15"
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
    }
}
