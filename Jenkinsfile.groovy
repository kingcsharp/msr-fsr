pipeline {
    options {
        disableConcurrentBuilds()
    }
    agent none
    environment {
        /*
        TARGET_ARN_DEV='arn:aws:elasticloadbalancing:us-east-1:536716547771:targetgroup/auditflix-dev/26111534632711a9'
        TARGET_ARN_STAGE='arn:aws:elasticloadbalancing:us-east-1:536716547771:targetgroup/auditflix-stage/4fec18820fab1417'
        TARGET_ARN_DEMO='arn:aws:elasticloadbalancing:us-east-1:536716547771:targetgroup/auditflix-demo/aa347c59f78697d1'
        TARGET_ARN_PROD='arn:aws:elasticloadbalancing:us-east-1:536716547771:targetgroup/auditflix-prod/e12c857846115550'
         */
        ACCOUNT_URL='425480257575.dkr.ecr.us-west-2.amazonaws.com'
        REGION='us-west-2'
        PROFILE='--profile msrfsr'
    }
    stages {
        stage('Build & Deploy') {
            parallel {
                stage('Build and Deploy UI Container') {
                    agent { label 'master'}
                    steps {
                        script {
                            dir('MSR.UI/MSR.UI.Answer') {
                                sh "sudo chmod 777 /var/run/docker.sock"
                                sh "docker build -f prod.Dockerfile -t msr-ui ."
                                sh "docker tag msr-ui ${ACCOUNT_URL}/msr-ui:${env.BRANCH_NAME}${env.BUILD_NUMBER}"

                                sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                sh "docker push ${ACCOUNT_URL}/msr-ui:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                            }

                            sh "sudo sh update_image.sh ${env.BRANCH_NAME} ${env.BUILD_NUMBER} docker-compose-ui.yml"
                            sh "cat docker-compose-ui.yml"

                            withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
                                sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

                                sh "ecs-cli compose --file docker-compose-ui.yml --project-name answer-ui service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-dev/1b3c1539f365fe8f --container-name app --container-port 80 --timeout 15"
                            }
                        }
                    }
                }

                stage('Build Reverse Proxy Container') {
                    agent { label 'master'}
                    steps {
                        script {
                            dir('reverseproxy') {
                                sh "sudo chmod 777 /var/run/docker.sock"
                                sh "docker build --build-arg NGINX_CONF=dev -t msr-rp ."
                                sh "docker tag msr-rp ${ACCOUNT_URL}/msr-rp:${env.BRANCH_NAME}${env.BUILD_NUMBER}"

                                sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                                sh "docker push ${ACCOUNT_URL}/msr-rp:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                            }

                            sh "sudo chmod 777 /var/run/docker.sock"
                            sh "git mv Msr.Infrastructure MSR.Infrastructure"
                            sh "docker build -f MSR.Answer.API/Dockerfile -t msr-api ."
                            sh "docker tag msr-api ${ACCOUNT_URL}/msr-api:${env.BRANCH_NAME}${env.BUILD_NUMBER}"

                            sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                            sh "docker push ${ACCOUNT_URL}/msr-api:${env.BRANCH_NAME}${env.BUILD_NUMBER}"

                            sh "sudo sh update_image_api.sh ${env.BRANCH_NAME} ${env.BUILD_NUMBER} docker-compose-api.yml"
                            sh "cat docker-compose-api.yml"

                            withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
                                sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

                                sh "ecs-cli compose --file docker-compose-api.yml --project-name answer-api service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api/397c455c0c042c71 --container-name reverseproxy --container-port 80 --timeout 15"
                            }

                        }
                    }
                }
            }
        }
    }
}
