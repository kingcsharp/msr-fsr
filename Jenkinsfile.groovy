pipeline {
    agent { label 'ws_build_agent'}
    options {
        disableConcurrentBuilds()
        timestamps()
        buildDiscarder(logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '', daysToKeepStr: '10', numToKeepStr: '10'))
    }
    environment {
        WEBHOOK_URL = 'https://outlook.office.com/webhook/19c3ea6c-d421-4b34-bf8b-9188e9e5c729@f139f56d-9238-4269-8e2e-8b0f314429cb/JenkinsCI/a8556c572acf47fdaa48888079cd22f5/73b18003-3808-48a4-bf4d-a0fc2650baa5'
        APP_NAME_DEV = 'AnswerDev'
        DEPLOY_GROUP_DEV = 'AnswerDevDeployment'
        APP_NAME_STAGE = 'AnswerStage'
        DEPLOY_GROUP_STAGE = 'AnswerDeploymentStage'
        APP_NAME_PROD = 'AnswerProd'
        DEPLOY_GROUP_PROD = 'AnswerProdDeployment'
        CATALOG_DEV = 'Answer2_Dev'
        CATALOG_STAGE = 'Answer2_Stage'
        CATALOG_PROD = 'Answer2_Prod'
        GREEN = '#008000'
        RED = '#FF0000'
    }
    stages {
        stage('Install Packages') {
            steps {
                script {
                    try {
                        //bat label: '', script: '.nuget\\Nuget.exe install packages.config -o packages'
                        bat label: '', script: '.nuget\\Nuget.exe restore Answer.Web\\packages.config -PackagesDirectory ..\\packages'
                    } catch(e) {
                        office365ConnectorSend color: "${RED}", message: "${JOB_NAME} build FAILED installing packages. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                        currentBuild.result = 'FAILURE'
                    }
                }
            }
        }
        stage("Build Msr.Database") {
            steps {
                script {
                    try {
                        if(env.JOB_NAME == "MSR-FSR/Answer2.0/stage") {
                            bat "\"${tool 'v2019'}\" Msr.Database/Msr.Database.sqlproj /t:Build /p:Configuration=Release"
                        } else {
                            echo "Not building database for ${env.JOB_NAME}"
                        }
                    } catch(e) {
                        office365ConnectorSend color: "${RED}", message: "${JOB_NAME} build FAILED building Msr.Database. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                        currentBuild.result = 'FAILURE'
                    }
                }
            }
        }
        /*
        stage("Generate SQL Script") {
            steps {
                script {
                    try {
                        if(env.JOB_NAME == "MSR-FSR/Answer2.0/stage" || env.JOB_NAME.startsWith("MSR-FSR/Answer2.0/Release/Release")) {
                            bat label: '', script: 'sqlpackage.exe /a:script /SourceFile:%WORKSPACE%\\Msr.Database\\bin\\Release\\Msr.Database.dacpac /TargetConnectionString:"Data Source=bang.msr-fsr.com;Initial Catalog=Answer2_Stage;User Id=sa;Password=L8xvg2FGqs7CEQ+a;Integrated Security=true" /OutputPath:temp.sql'
                        } else {
                            echo "Not building script for ${env.JOB_NAME}"
                        }
                    } catch(e) {
                        office365ConnectorSend color: "${RED}", message: "${JOB_NAME} build FAILED generating SQL script from Msr.Database. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                        currentBuild.result = 'FAILURE'
                    }
                }
            }
        }
        stage("Publish SQL Script") {
            steps {
                script {
                    try {
                        if(env.JOB_NAME == "MSR-FSR/Answer2.0/stage") {
                            bat label: '', script: 'sqlpackage.exe /a:publish /SourceFile:%WORKSPACE%\\Msr.Database\\bin\\Release\\Msr.Database.dacpac /TargetConnectionString:"Data Source=bang.msr-fsr.com;Initial Catalog=Answer2_Stage;User Id=sa;Password=L8xvg2FGqs7CEQ+a;Integrated Security=true"'
                        } else {
                            echo "Only publishing a script for Stage"
                        }
                    } catch(e) {
                        office365ConnectorSend color: "${RED}", message: "${JOB_NAME} build FAILED publishing SQL script from Msr.Database. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                    }
                }
            }
        }
        */
        stage("Process Transforms") {
            steps {
                script {
                    try {
                        if(env.JOB_NAME == "MSR-FSR/Answer2.0/stage") {
                            bat "\"${tool 'v14-amd64'}\" Answer.Web\\transform.stage.proj /t:Stage"
                        } else if(env.JOB_NAME == "MSR-FSR/Answer2.0/master") {
                            bat "\"${tool 'v14-amd64'}\" Answer.Web\\transform.prod.proj /t:Prod"
                        } else {
                            bat "\"${tool 'v14-amd64'}\" Answer.Web\\transform.dev.proj /t:Dev"
                        }
                    } catch(e) {
                        office365ConnectorSend color: "${RED}", message: "${JOB_NAME} build FAILED processing transforms. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                        currentBuild.result = 'FAILURE'
                    }
                }
            }
        }
        stage("Build Answer.Web") {
            steps {
                script {
                    try {
                        bat "\"${tool 'v14-amd64'}\" Answer.Web/Answer.Web.csproj /t:Build /p:Configuration=Release /p:DeployOnBuild=true /p:OutputPath=${WORKSPACE}/publish"
                    } catch(e) {
                        office365ConnectorSend color: "${RED}", message: "${JOB_NAME} build FAILED building Answer.Web. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                        currentBuild.result = 'FAILURE'
                    }
                }
            }
        }
        stage("Packaging release") {
            steps {
                script {
                    try {
                        bat label: '', script: 'xcopy /y %WORKSPACE%\\appspec.yml %WORKSPACE%\\publish\\_PublishedWebsites\\'
                        bat label: '', script: 'xcopy /y %WORKSPACE%\\deployment\\pre.bat %WORKSPACE%\\publish\\_PublishedWebsites\\'
                        bat label: '', script: 'xcopy /y %WORKSPACE%\\deployment\\post.bat %WORKSPACE%\\publish\\_PublishedWebsites\\'
                        bat label: '', script: 'md "%WORKSPACE%\\publish\\_PublishedWebsites\\Answer.Web/bin\\roslyn"'
                        bat label: '', script: 'xcopy /s %WORKSPACE%\\Roslyn45 %WORKSPACE%\\publish\\_PublishedWebsites\\Answer.Web\\bin\\roslyn'
                        //bat label: '', script: 'md "%WORKSPACE%\\publish\\_PublishedWebsites\\Answer.Web\\App_Start"'
                        //bat label: '', script: 'xcopy /s %WORKSPACE%\\Answer.Web\\App_Start %WORKSPACE%\\publish\\_PublishedWebsites\\Answer.Web\\App_Start'
                        //bat label: '', script: 'xcopy /y %WORKSPACE%\\Answer.Web\\bundleconfig.json %WORKSPACE%\\publish\\_PublishedWebsites\\Answer.Web\\'
                        bat label: '', script: 'd:\\tools\\7-Zip\\7z a %WORKSPACE%\\publish\\publish.zip publish\\_PublishedWebsites\\'
                    } catch(e) {
                        office365ConnectorSend color: "${RED}", message: "${JOB_NAME} build FAILED packaging the release. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                        currentBuild.result = 'FAILURE'
                    }
                }
            }
        }
        stage("Deploy to server") {
            steps {
                script {
                    script {
                        try {
                            if(env.JOB_NAME == "MSR-FSR/Answer2.0/dev") {
                                deploy("${APP_NAME_DEV}","${DEPLOY_GROUP_DEV}")
                            } else if(env.JOB_NAME == "MSR-FSR/Answer2.0/stage") {
                                deploy("${APP_NAME_STAGE}","${DEPLOY_GROUP_STAGE}")
                            } else if(env.JOB_NAME == "MSR-FSR/Answer2.0/master") {
                                deploy("${APP_NAME_PROD}","${DEPLOY_GROUP_PROD}")
                            } else {
                                echo "There is no deployment for this branch."
                            }
                        } catch(e) {
                            office365ConnectorSend color: "${RED}", message: "${JOB_NAME} build FAILED deploying to server. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
                            currentBuild.result = 'FAILURE'
                        }
                    }

                    office365ConnectorSend color: "${GREEN}", message: "${JOB_NAME} build completed.", status: 'Passed',webhookUrl: "${WEBHOOK_URL}"
                }
            }
        }
        /*
        stage("Notify Rollbar") {
            agent { label 'master'}
            steps {
                script {
                    notify()
                }
            }
        }
        */
    }
}

void deploy(appName,deployName) {
    withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: '0ecdc6b1-481b-4af0-8897-ad528b954c49', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
        step([$class: 'AWSCodeDeployPublisher', applicationName: "${appName}",
              awsAccessKey: "${AWS_ACCESS_KEY_ID}", awsSecretKey: "${AWS_SECRET_ACCESS_KEY}",
              credentials: 'awsAccessKey', deploymentConfig: 'CodeDeployDefault.OneAtATime',
              deploymentGroupAppspec: false, deploymentGroupName: "${deployName}",
              deploymentMethod: 'deploy', excludes: '', iamRoleArn: '', includes: '**',
              pollingFreqSec: 15, pollingTimeoutSec: 300, proxyHost: '', proxyPort: 0, region: 'us-west-2',
              s3bucket: 'answer-deployments', s3prefix: 'publish', subdirectory: 'publish\\_PublishedWebsites',
              versionFileName: '', waitForCompletion: true])
    }
}

void notify(branch, commit) {
    sh "curl https://api.rollbar.com/api/1/deploy/ -F access_token=c505bac8121c489cb9fbfad0f4dc880d -F environment=${branch} -F revision=${commit} -F local_username=system"
}
