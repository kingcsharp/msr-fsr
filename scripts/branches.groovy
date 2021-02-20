pipeline {
    agent { docker { image 'docker' } }
    stages {
        stage('build') {
            steps {
                //try {
                    dir('reverseproxy') {
                        echo "Building api proxy container...."
                        sh "docker build --build-arg NGINX_CONF=dev -t msr-rp ."
                        sh "docker tag msr-rp ${ACCOUNT_URL}/msr-rp:${env.GIT_COMMIT}"

                        sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                        sh "docker push ${ACCOUNT_URL}/msr-rp:${env.GIT_COMMIT}"
                    }
//                } catch(e) {
//                    office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the NGINX image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
//                    currentBuild.result = 'FAILURE'
//                    sh "exit 1"
//                }

                //try {
                    echo "Building API container...."
                    sh "docker build -f MSR.Answer.API/Dockerfile -t msr-api ."
                    sh "docker tag msr-api ${ACCOUNT_URL}/msr-api:${env.GIT_COMMIT}"

                    sh "eval \$(/snap/bin/aws ecr get-login --region ${REGION} --no-include-email ${PROFILE} | sed 's|https://||')"
                    sh "docker push ${ACCOUNT_URL}/msr-api:${env.GIT_COMMIT}"

//                } catch(e) {
//                    office365ConnectorSend color: "${RED}", message: "${env.BRANCH_NAME} build FAILED building the API image. \n Error: ${e}", status: 'Failed', webhookUrl: "${WEBHOOK_URL}"
//                    currentBuild.result = 'FAILURE'
//                    sh "exit 1"
//                }
            }
        }
    }
}