pipeline {
    options {
        disableConcurrentBuilds()
    }
    agent { label 'master'}
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
        stage('Build UI Container') {
            steps {
                script {
                    dir('MSR.UI/MSR.UI.Answer') {
                        sh "sudo chmod 777 /var/run/docker.sock"
                        sh "docker build -t msr-ui ."
                        sh "docker tag msr-ui ${ACCOUNT_URL}/msr-ui:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                    }
                }
            }
        }

        stage('Push UI image to AWS ECR') {
            steps {
                script {
                    dir('MSR.UI/MSR.UI.Answer') {
                        sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                        sh "docker push ${ACCOUNT_URL}/msr-ui:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                    }
                }
            }
        }

        stage('Build Reverse Proxy Container') {
            steps {
                script {
                    dir('reverseproxy') {
                        sh "sudo chmod 777 /var/run/docker.sock"
                        sh "docker build -t msr-rp ."
                        sh "docker tag msr-rp ${ACCOUNT_URL}/msr-rp:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                    }
                }
            }
        }

        stage('Push Reverse Proxy image to AWS ECR') {
            steps {
                script {
                    dir('reverseproxy') {
                        sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                        sh "docker push ${ACCOUNT_URL}/msr-rp:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                    }
                }
            }
        }
        /*
        stage('Build API Container') {
            steps {
                script {
                    sh "sudo chmod 777 /var/run/docker.sock"
                    sh "docker build -f MSR.Answer.API/Dockerfile -t msr-api ."
                    sh "docker tag msr-api ${ACCOUNT_URL}/msr-api:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                }
            }
        }

        stage('Push API image to AWS ECR') {
            steps {
                script {
                    sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                    sh "docker push ${ACCOUNT_URL}/msr-api:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                }
            }
        }
        */
        stage('Update docker-compose file') {
            steps {
                script {
                    sh "sudo ./update_image.sh ${env.BRANCH_NAME} ${env.BUILD_NUMBER} docker-compose-dev.yml"
                }
            }
        }

        stage("Deploy to ECS") {
            steps {
                script {
                    withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                        sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
                        sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

                        sh "ecs-cli compose --file docker-compose-dev.yml --project-name answer-ui service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-dev/1b3c1539f365fe8f --container-name app --container-port 80 --timeout 10"
                    }
                }
            }
        }
    }
}
