pipeline {
    options {
        disableConcurrentBuilds()
    }
    agent none
    environment {
        WEBHOOK_URL = 'https://outlook.office.com/webhook/19c3ea6c-d421-4b34-bf8b-9188e9e5c729@f139f56d-9238-4269-8e2e-8b0f314429cb/JenkinsCI/a8556c572acf47fdaa48888079cd22f5/73b18003-3808-48a4-bf4d-a0fc2650baa5'
        GREEN = '#008000'
        RED = '#FF0000'
    }
    stages {
        stage('Build & Deploy') {
            parallel {
                stage('Build Answer UI') {
                    agent { label 'jnlp'}
                    steps {
                        script {

                            try {
                                echo "GIT COMMIT HASH: ${env.GIT_COMMIT}"
                                withCredentials([usernamePassword(credentialsId: 'aws-msrfsr-key-secret', passwordVariable: 'PASS', usernameVariable: 'KEY')]) {
                                    awsCodeBuild credentialsType: 'keys',
                                            awsAccessKey: "${KEY}",
                                            awsSecretKey: "${PASS}",
                                            projectName: 'github-pr-branches',
                                            region: "us-west-2",
                                            sourceControlType: 'jenkins',
                                            sourceTypeOverride: 'S3',
                                            sourceLocationOverride: 'answer-codebuild-input/branch-ui-qa.zip',
                                            buildSpecFile: 'scripts/buildspec-branches-answer-ui.yml',
                                            envVariables: "[{TAG, ${env.GIT_COMMIT}}]"
                                }
                            }
                            catch (e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the Answer UI. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }
                        }
                    }
                }

                stage('Build Portal UI') {
                    agent { label 'jnlp'}
                    steps {
                        script {

                            try {
                                echo "GIT COMMIT HASH: ${env.GIT_COMMIT}"
                                withCredentials([usernamePassword(credentialsId: 'aws-msrfsr-key-secret', passwordVariable: 'PASS', usernameVariable: 'KEY')]) {
                                    awsCodeBuild credentialsType: 'keys',
                                            awsAccessKey: "${KEY}",
                                            awsSecretKey: "${PASS}",
                                            projectName: 'github-pr-branches',
                                            region: "us-west-2",
                                            sourceControlType: 'jenkins',
                                            sourceTypeOverride: 'S3',
                                            sourceLocationOverride: 'answer-codebuild-input/branch-portal-ui-qa.zip',
                                            buildSpecFile: 'scripts/buildspec-branches-portal-ui.yml',
                                            envVariables: "[{TAG, ${env.GIT_COMMIT}}]"
                                }
                            }
                            catch (e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the Portal UI. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }
                        }
                    }
                }

                stage('Build API') {
                    agent { label 'jnlp' }
                    steps {
                        script {

                            try {
                                echo "GIT COMMIT HASH: ${env.GIT_COMMIT}"
                                withCredentials([usernamePassword(credentialsId: 'aws-msrfsr-key-secret', passwordVariable: 'PASS', usernameVariable: 'KEY')]) {
                                    awsCodeBuild credentialsType: 'keys',
                                            awsAccessKey: "${KEY}",
                                            awsSecretKey: "${PASS}",
                                            projectName: 'github-pr-branches',
                                            region: "us-west-2",
                                            sourceControlType: 'jenkins',
                                            sourceTypeOverride: 'S3',
                                            sourceLocationOverride: 'answer-codebuild-input/branch-api.zip',
                                            buildSpecFile: 'scripts/buildspec-branches-api.yml',
                                            envVariables: "[{TAG, ${env.GIT_COMMIT}}]"
                                }
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building API. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }
                        }
                    }
                }

                stage('Build Processor') {
                    agent { label 'jnlp'}
                    steps {
                        script {
                            echo "Building Processor container...."

                            try {
                                withCredentials([usernamePassword(credentialsId: 'aws-msrfsr-key-secret', passwordVariable: 'PASS', usernameVariable: 'KEY')]) {
                                    awsCodeBuild credentialsType: 'keys',
                                            awsAccessKey: "${KEY}",
                                            awsSecretKey: "${PASS}",
                                            projectName: 'github-pr-branches',
                                            region: "us-west-2",
                                            sourceControlType: 'jenkins',
                                            sourceTypeOverride: 'S3',
                                            sourceLocationOverride: 'answer-codebuild-input/branch-processor.zip',
                                            buildSpecFile: 'scripts/buildspec-branches-processor.yml',
                                            envVariables: "[{TAG, ${env.GIT_COMMIT}}]"
                                }
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the Processor. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                                currentBuild.result = 'FAILURE'
                                sh "exit 1"
                            }
                        }
                    }
                }

                stage('Build MessageHub') {
                    agent { label 'jnlp'}
                    steps {
                        script {

                            try {
                                echo "GIT COMMIT HASH: ${env.GIT_COMMIT}"
                                withCredentials([usernamePassword(credentialsId: 'aws-msrfsr-key-secret', passwordVariable: 'PASS', usernameVariable: 'KEY')]) {
                                    awsCodeBuild credentialsType: 'keys',
                                            awsAccessKey: "${KEY}",
                                            awsSecretKey: "${PASS}",
                                            projectName: 'github-pr-branches',
                                            region: "us-west-2",
                                            sourceControlType: 'jenkins',
                                            sourceTypeOverride: 'S3',
                                            sourceLocationOverride: 'answer-codebuild-input/branch-message.zip',
                                            buildSpecFile: 'scripts/buildspec-branches-message.yml',
                                            envVariables: "[{TAG, ${env.GIT_COMMIT}}]"
                                }
                            } catch(e) {
                                office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the Message. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
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
