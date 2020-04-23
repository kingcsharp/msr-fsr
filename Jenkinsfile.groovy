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

        stage('Push image to AWS ECR') {
            steps {
                script {
                    dir('MSR.UI/MSR.UI.Answer') {
                        sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                        sh "docker push ${ACCOUNT_URL}/msr-ui:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                    }
                }
            }
        }

        stage('Build API Container') {
            steps {
                script {
                    sh "sudo chmod 777 /var/run/docker.sock"
                    sh "docker build -t msr-api ."
                    sh "docker tag msr-api ${ACCOUNT_URL}/msr-api:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                }
            }
        }

        stage('Push image to AWS ECR') {
            steps {
                script {
                    sh "eval \$(/home/ubuntu/.local/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                    sh "docker push ${ACCOUNT_URL}/msr-api:${env.BRANCH_NAME}${env.BUILD_NUMBER}"
                }
            }
        }
        /*
        stage('Update docker-compose file') {
            steps {
                script {
                    if (env.BRANCH_NAME == "Dev") {
                        sh "sudo ./update_image.sh ${env.BRANCH_NAME} ${env.BUILD_NUMBER} docker-compose-dev.yml"
                        sh "cat docker-compose-dev.yml"
                    } else if (env.BRANCH_NAME == "Stage") {
                        sh "sudo ./update_image.sh ${env.BRANCH_NAME} ${env.BUILD_NUMBER} docker-compose-stage.yml"
                        sh "cat docker-compose-stage.yml"
                    } else if (env.BRANCH_NAME == "master") {
                        sh "sudo ./update_image.sh ${env.BRANCH_NAME} ${env.BUILD_NUMBER} docker-compose-prod.yml"
                        sh "sudo ./update_image.sh demo ${env.BUILD_NUMBER} docker-compose-demo.yml"
                        sh "cat docker-compose-prod.yml"
                        sh "cat docker-compose-demo.yml"
                    }
                }
            }
        }
        stage("Deploy to ECS") {
            steps {
                script {
                    withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: '4b98a157-7f0c-4e31-a49b-da3754a37f02', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                        sh "ecs-cli configure --cluster auditflix --default-launch-type FARGATE --config-name auditflix-config --region us-east-1"
                        sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name auditflix-profile"

                        if (env.BRANCH_NAME == "Dev") {
                            sh "ecs-cli compose --file docker-compose-dev.yml --project-name auditflix-dev service up --create-log-groups --cluster-config auditflix-config --ecs-profile auditflix-profile --target-group-arn ${TARGET_ARN_DEV} --container-name app --container-port 80 --timeout 10"
                        } else if (env.BRANCH_NAME == "Stage") {
                            sh "ecs-cli compose --file docker-compose-stage.yml --project-name auditflix-stage service up --create-log-groups --cluster-config auditflix-config --ecs-profile auditflix-profile --target-group-arn ${TARGET_ARN_STAGE} --container-name app --container-port 80 --timeout 10"
                        } else if (env.BRANCH_NAME == "master") {
                            sh "ecs-cli compose --file docker-compose-demo.yml --project-name auditflix-demo service up --create-log-groups --cluster-config auditflix-config --ecs-profile auditflix-profile --target-group-arn ${TARGET_ARN_DEMO} --container-name app --container-port 80 --timeout 10"
                            sh "ecs-cli compose --file docker-compose-prod.yml --project-name auditflix-prod service up --create-log-groups --cluster-config auditflix-config --ecs-profile auditflix-profile --target-group-arn ${TARGET_ARN_PROD} --container-name app --container-port 80 --timeout 10"
                        }
                    }
                }
            }
        }
        */
    }
}
