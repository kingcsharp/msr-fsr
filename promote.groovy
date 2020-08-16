pipeline {
    options {
        disableConcurrentBuilds()
    }
    agent { label 'master' }
    environment {
        WEBHOOK_URL = 'https://outlook.office.com/webhook/19c3ea6c-d421-4b34-bf8b-9188e9e5c729@f139f56d-9238-4269-8e2e-8b0f314429cb/JenkinsCI/a8556c572acf47fdaa48888079cd22f5/73b18003-3808-48a4-bf4d-a0fc2650baa5'
        GREEN = '#008000'
        RED = '#FF0000'
        ACCOUNT_URL='425480257575.dkr.ecr.us-west-2.amazonaws.com'
        REGION='us-west-2'
        PROFILE='--profile msrfsr'
        API_COMPOSE='docker-compose-api.yml'
        UI_COMPOSE='docker-compose-ui.yml'
        STAGE_PROJECT_API='stage-answer-api'
        STAGE_PROJECT_UI='stage-answer-ui'
        STAGE_API_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api-stage/e7d741c03c9de262"
        STAGE_UI_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-ui-stage/3a5df8140101b695"
    }
    properties([
            parameters([
                    string(name: 'DEPLOY_ENV', defaultValue: 'STAGE', description: 'The target environment', )
            ])
    ])
    stages {
        stage("Get images") {
            steps {
                script {
                    echo "Deploying to ${DEPLOY_ENV}"
                    sh "/home/ubuntu/.local/bin/aws ecs describe-task-definition --task-definition dev-answer-api --profile msrfsr --region us-west-2 > images.json"
                    def props = readJSON file: 'images.json'
                    def apiImage = props['taskDefinition']['containerDefinitions'][0].image
                    String[] api
                    api = apiImage.split(':')
                    sh "sudo sh update_image.sh dev ${api[1]} ${UI_COMPOSE}"
                    sh "cat ${UI_COMPOSE}"
                    sh "sudo sh update_image_api.sh dev ${api[1]} ${API_COMPOSE}"
                    sh "cat ${API_COMPOSE}"
                }
            }
        }
        stage('Build & Deploy') {
            parallel {
                stage("Promoting UI to Stage") {
                    steps {
                        script {
                            deploy("${UI_COMPOSE}", "${STAGE_PROJECT_UI}", "${STAGE_UI_TARGET_ARN}", "app")
                        }
                    }
                }
                stage("Promoting API to Stage") {
                    steps {
                        script {
                            deploy("${API_COMPOSE}", "${STAGE_PROJECT_API}", "${STAGE_API_TARGET_ARN}", "reverseproxy")
                        }
                    }
                }
            }
        }
    }
}

void deploy(composeFile,name,target, app) {
    withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'msrfsr-aws-jenkins', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
        sh "ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-west-2"
        sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"

        sh "ecs-cli compose --file ${composeFile} --project-name ${name} service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn ${target} --container-name ${app} --container-port 80 --timeout 15"
    }
}