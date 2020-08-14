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
        stage("Get images") {
            agent { label 'master' }
            steps {
                script {
                    sh "aws ecs describe-task-definition --task-definition dev-answer-api > images.json --profile msrfsr"
                    sh "cat images.json"
                }
            }
        }
    }
}