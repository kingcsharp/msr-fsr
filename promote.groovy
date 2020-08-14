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
        API_COMPOSE='docker-compose-api.yml'
        UI_COMPOSE='docker-compose-ui.yml'
    }
    stages {
        stage("Get images") {
            agent { label 'master' }
            steps {
                script {
                    sh "/home/ubuntu/.local/bin/aws ecs describe-task-definition --task-definition dev-answer-api --profile msrfsr --region us-west-2 > images.json"
                    sh "cat images.json"
                    def props = readJSON file: 'images.json'
                    println(props['taskDefinition']['containerDefinitions'][0].image)
                    def apiImage = props['taskDefinition']['containerDefinitions'][0].image
                    //def rpImage = props['taskDefinition']['containerDefinitions'][1].image
                    String[] api
                    api = apiImage.split(':')
                    println(api[1])
                    sh "sudo sh update_image.sh dev ${api[1]} ${UI_COMPOSE}"
                    sh "cat ${UI_COMPOSE}"
                    sh "sudo sh update_image_api.sh dev ${api[1]} ${API_COMPOSE}"
                    sh "cat ${API_COMPOSE}"

                }
            }
        }
    }
}