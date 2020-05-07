## Run command to create role to execute tasks on AWS Fargate
`aws iam --region us-east-1 create-role --role-name ecsTaskExecutionRole --assume-role-policy-document file://task-execution-assume-role.json` <br/>

## Attach the task execution policy
`aws iam --region us-east-1 attach-role-policy --role-name ecsTaskExecutionRole --policy-arn arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy`

## Create the cluster configuration
`ecs-cli configure --cluster answer --default-launch-type FARGATE --config-name answer-config --region us-east-1`

## Create ECS cli profile
`ecs-cli configure profile --access-key AWS_ACCESS_KEY_ID --secret-key AWS_SECRET_ACCESS_KEY --profile-name answer-profile`

## Deployment UI ##
`ecs-cli compose --file docker-compose-dev.yml --project-name answer-ui service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-dev/1b3c1539f365fe8f --container-name app --container-port 80`

#API
`ecs-cli compose --file docker-compose-api.yml --project-name answer-api service up --create-log-groups --cluster-config answer-config --ecs-profile answer-profile --target-group-arn arn:aws:elasticloadbalancing:us-west-2:425480257575:targetgroup/answer3-api/397c455c0c042c71 --container-name app --container-port 80`
