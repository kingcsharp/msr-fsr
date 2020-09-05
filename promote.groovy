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
        QA_PROJECT_API='dev-answer-api'
        QA_PROJECT_UI='dev-answer-ui'
        STAGE_PROJECT_API='stage-answer-api'
        STAGE_PROJECT_UI='stage-answer-ui'
        STAGE_API_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api-stage/e7d741c03c9de262"
        STAGE_UI_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-ui-stage/3a5df8140101b695"
        PROD_PROJECT_API='prod-answer-api'
        PROD_PROJECT_UI='prod-answer-ui'
        PROD_API_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api-prod/0d264f923b3ca5c5"
        PROD_UI_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-ui-prod/a05ebf3f959d039b"
    }
    parameters {
        //string(name: 'DEPLOY_ENV', defaultValue: 'STAGE', description: 'The target environment',)
        choice(name: 'DEPLOY_ENV', choices: ['STAGE', 'PRODUCTION'], description: 'Choose an environment')
    }
    stages {
        stage("Get images") {
            steps {
                script {
                    echo "Deploying to ${DEPLOY_ENV}"
                    if(DEPLOY_ENV == "STAGE")  {
                        sh "/snap/bin/aws ecs describe-task-definition --task-definition ${QA_PROJECT_API} --profile msrfsr --region us-west-2 > images.json"
                        def props = readJSON file: 'images.json'
                        def apiImage = props['taskDefinition']['containerDefinitions'][0].image
                        String[] api
                        api = apiImage.split(':')
                        sh "sh update_image.sh Stage ${api[1]} ${UI_COMPOSE}"
                        sh "cat ${UI_COMPOSE}"
                        sh "sh update_image_api.sh Stage ${api[1]} ${API_COMPOSE}"
                        sh "cat ${API_COMPOSE}"
                    } else if(DEPLOY_ENV == "PRODUCTION") {
                        sh "/snap/bin/aws ecs describe-task-definition --task-definition ${STAGE_PROJECT_API} --profile msrfsr --region us-west-2 > images.json"
                        def props = readJSON file: 'images.json'
                        def apiImage = props['taskDefinition']['containerDefinitions'][0].image
                        String[] api
                        api = apiImage.split(':')
                        sh "sh update_image.sh Production ${api[1]} ${UI_COMPOSE}"
                        sh "cat ${UI_COMPOSE}"
                        sh "sh update_image_api.sh Production ${api[1]} ${API_COMPOSE}"
                        sh "cat ${API_COMPOSE}"
                    } else {
                        echo "Only promoting stage and production!"
                    }
                }
            }
        }
        stage('Build & Deploy') {
            parallel {
                stage("Promoting UI to Stage") {
                    steps {
                        script {
                            if(DEPLOY_ENV == "STAGE") {

                                try {
                                    dir('MSR.UI/MSR.UI.Answer') {
                                        //sh "sudo chmod 777 /var/run/docker.sock"
                                        sh "docker build --build-arg ENV=dev -t msr-ui ."
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
                                    sh "sh update_image.sh ${env.BRANCH_NAME} ${env.GIT_COMMIT} ${UI_COMPOSE}"
                                    sh "cat ${UI_COMPOSE}"

                                    deploy("${UI_COMPOSE}", "${STAGE_PROJECT_UI}", "${STAGE_UI_TARGET_ARN}", "app")

                                    office365ConnectorSend color: "${GREEN}", message: "${env.BRANCH_NAME} UI deployed successfully.", status: 'Passed',webhookUrl: "${WEBHOOK_URL}"

                                } catch (e) {
                                    office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED deploying the UI containers. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                    currentBuild.result = 'FAILURE'
                                    sh "exit 1"
                                }

                            } else if (DEPLOY_ENV == "PRODUCTION") {
                                deploy("${UI_COMPOSE}", "${PROD_PROJECT_UI}", "${PROD_UI_TARGET_ARN}", "app")
                            }else {
                                echo "Only promoting stage and production!"
                            }
                        }
                    }
                }
                stage("Promoting API to Stage") {
                    steps {
                        script {
                            if(DEPLOY_ENV == "STAGE") {
                                deploy("${API_COMPOSE}", "${STAGE_PROJECT_API}", "${STAGE_API_TARGET_ARN}", "reverseproxy")
                            } else if (DEPLOY_ENV == "PRODUCTION") {
                                deploy("${API_COMPOSE}", "${PROD_PROJECT_API}", "${PROD_API_TARGET_ARN}", "reverseproxy")
                            } else {
                                echo "Only promoting stage and production!"
                            }
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
        //sh "ecs-cli configure profile --access-key ${AWS_ACCESS_KEY_ID} --secret-key ${AWS_SECRET_ACCESS_KEY} --profile-name answer-profile"
        sh "ecs-cli configure profile --access-key AKIAWGEEZQATRVZEHMGB --secret-key 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 --profile-name answer-profile"

        sh "ecs-cli compose --file ${composeFile} --project-name ${name} service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn ${target} --container-name ${app} --container-port 80 --timeout 15"
    }
}