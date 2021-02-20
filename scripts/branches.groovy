pipeline {
    agent { label 'master'}
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
        QA_MESSAGE_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-message-qa/a77318365ce97cd4"
        STAGE_MESSAGE_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-message-stage/4397bae7ec33c820"
        PROD_MESSAGE_TARGET_ARN="arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-message-prod/234f938c0ca82cb1"
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
        stage('build') {
            steps {
                //try {
                    dir('reverseproxy') {
                        echo "Building api proxy container...."
                        sh "docker build --build-arg NGINX_CONF=dev -t msr-rp ."
                    }
//                } catch(e) {
//                    office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the NGINX image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
//                    currentBuild.result = 'FAILURE'
//                    sh "exit 1"
//                }

                //try {
                    echo "Building API container...."
                    sh "docker build -f MSR.Answer.API/Dockerfile -t msr-api ."


//                } catch(e) {
//                    office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the API image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
//                    currentBuild.result = 'FAILURE'
//                    sh "exit 1"
//                }
            }
        }
    }
}