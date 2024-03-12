import * as cdk from "aws-cdk-lib";
import * as cf from "aws-cdk-lib/aws-cloudfront";
import * as iam from "aws-cdk-lib/aws-iam";
import * as ecr from "aws-cdk-lib/aws-ecr";
import * as s3 from "aws-cdk-lib/aws-s3";
import * as sm from "aws-cdk-lib/aws-secretsmanager";
import { Construct } from "constructs";


export class Answer30Stack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    const codeBuildPermissions = [
      "logs:CreateLogGroup",
      "logs:CreateLogStream",
      "logs:PutLogEvents",
      "s3:PutObject",
      "s3:GetObject",
      "s3:GetObjectVersion",
      "s3:GetBucketAcl",
      "s3:GetBucketLocation",
      "codebuild:CreateReportGroup",
      "codebuild:CreateReport",
      "codebuild:UpdateReport",
      "codebuild:BatchPutTestCases",
      "codebuild:BatchPutCodeCoverages",
    ];

    const ecsPermissions = [
      "ecs:UpdateService",
      "ecs:DescribeServices",
      "ecs:UpdateServicePrimaryTaskSet",
      "ecs:CreateTaskSet",
      "logs:CreateLogGroup",
      "ecs:RegisterTaskDefinition",
    ];

    const uicbr = new iam.Role(this, "uicbr", {
      assumedBy: new iam.ServicePrincipal("codebuild.amazonaws.com"),
    });

    const uiqas3b = s3.Bucket.fromBucketName(this, "uiqas3b", "answer-ui-qa");

    uiqas3b.grantReadWrite(uicbr);

    const uiqacf = cf.Distribution.fromDistributionAttributes(this, "uiqacf", {
      distributionId: "E2LJS1DYU10ZJH",
      domainName: "dq4p6lbe8guih.cloudfront.net",
    });

    uiqacf.grantCreateInvalidation(uicbr);

    uicbr.addToPolicy(new iam.PolicyStatement({
      effect: iam.Effect.ALLOW,
      resources: [
        "arn:aws:logs:us-west-2:425480257575:log-group:/aws/codebuild/Answer30-UI-dev:*",
        "arn:aws:logs:us-west-2:425480257575:log-group:/aws/codebuild/Answer30-UI-dev",
        "arn:aws:s3:::codepipeline-us-west-2-*",
        "arn:aws:codebuild:us-west-2:425480257575:report-group/Answer30-UI-dev-*",
      ],
      actions: codeBuildPermissions,
    }));

    const apicbr = new iam.Role(this, "apicbr", {
      assumedBy: new iam.ServicePrincipal("codebuild.amazonaws.com"),
    });

    const dockerHubCreds = sm.Secret.fromSecretNameV2(this, 'dockerHubCreds', 'dockerhub');

    dockerHubCreds.grantRead(apicbr);

    const msrRpRepo = ecr.Repository.fromRepositoryName(this, "msrRpRepo", "msr-rp");

    msrRpRepo.grantPush(apicbr);

    const msrApiRepo = ecr.Repository.fromRepositoryName(this, "msrApiRepo", "msr-api");

    msrApiRepo.grantPush(apicbr);

    const ecsTaskExecRole = iam.Role.fromRoleName(this, "ecsTaskExecRole", "ecsTaskExecutionRole");

    ecsTaskExecRole.grantPassRole(apicbr);

    apicbr.addToPolicy(new iam.PolicyStatement({
      effect: iam.Effect.ALLOW,
      resources: [
        "arn:aws:ecs:us-west-2:425480257575:service/answer-qa/qa-answer-api",
        "arn:aws:ecs:us-west-2:425480257575:task-definition/qa-answer-api:*",
        "arn:aws:logs:us-west-2:425480257575:log-group:answer3",
        "arn:aws:logs:us-west-2:425480257575:log-group:answer3:*",
      ],
      actions: ecsPermissions,
    }));

    apicbr.addToPolicy(new iam.PolicyStatement({
      effect: iam.Effect.ALLOW,
      resources: [
        "arn:aws:logs:us-west-2:425480257575:log-group:/aws/codebuild/Answer30-API-dev",
        "arn:aws:logs:us-west-2:425480257575:log-group:/aws/codebuild/Answer30-API-dev:*",
        "arn:aws:s3:::codepipeline-us-west-2-*",
        "arn:aws:codebuild:us-west-2:425480257575:report-group/Answer30-API-dev-*",
      ],
      actions: codeBuildPermissions,
    }));

    const mhcbr = new iam.Role(this, "mhcbr", {
      assumedBy: new iam.ServicePrincipal("codebuild.amazonaws.com"),
    });

    dockerHubCreds.grantRead(mhcbr);

    ecsTaskExecRole.grantPassRole(mhcbr);

    const msrMpRepo = ecr.Repository.fromRepositoryName(this, "msrMpRepo", "msr-mp");

    msrMpRepo.grantPush(mhcbr);

    const msrMessageRepo = ecr.Repository.fromRepositoryName(this, "msrMessageRepo", "msr-message");

    msrMessageRepo.grantPush(mhcbr);

    mhcbr.addToPolicy(new iam.PolicyStatement({
      effect: iam.Effect.ALLOW,
      resources: [
        "arn:aws:ecs:us-west-2:425480257575:service/answer-qa/qa-answer-message",
        "arn:aws:ecs:us-west-2:425480257575:task-definition/qa-answer-message:*",
        "arn:aws:logs:us-west-2:425480257575:log-group:answer3:*",
        "arn:aws:logs:us-west-2:425480257575:log-group:answer3",
      ],
      actions: ecsPermissions,
    }));

    mhcbr.addToPolicy(new iam.PolicyStatement({
      effect: iam.Effect.ALLOW,
      resources: [
        "arn:aws:logs:us-west-2:425480257575:log-group:/aws/codebuild/Answer30-MessageHub-dev",
        "arn:aws:logs:us-west-2:425480257575:log-group:/aws/codebuild/Answer30-MessageHub-dev:*",
        "arn:aws:s3:::codepipeline-us-west-2-*",
        "arn:aws:codebuild:us-west-2:425480257575:report-group/Answer30-MessageHub-dev-*",
      ],
      actions: codeBuildPermissions,
    }));

    const pcbr = new iam.Role(this, "pcbr", {
      assumedBy: new iam.ServicePrincipal("codebuild.amazonaws.com"),
    });

    dockerHubCreds.grantRead(pcbr);

    ecsTaskExecRole.grantPassRole(pcbr);

    const msrProcessorRepo = ecr.Repository.fromRepositoryName(this, "msrProcessorRepo", "msr-processor");

    msrProcessorRepo.grantPush(pcbr);

    pcbr.addToPolicy(new iam.PolicyStatement({
      effect: iam.Effect.ALLOW,
      resources: [
        "arn:aws:ecs:us-west-2:425480257575:service/answer-qa/qa-answer-api-processor",
        "arn:aws:ecs:us-west-2:425480257575:task-definition/qa-answer-api-processor:*",
        "arn:aws:logs:us-west-2:425480257575:log-group:answer3:*",
        "arn:aws:logs:us-west-2:425480257575:log-group:answer3",
      ],
      actions: ecsPermissions,
    }));

    pcbr.addToPolicy(new iam.PolicyStatement({
      effect: iam.Effect.ALLOW,
      resources: [
        "arn:aws:logs:us-west-2:425480257575:log-group:/aws/codebuild/Answer30-Processor-dev",
        "arn:aws:logs:us-west-2:425480257575:log-group:/aws/codebuild/Answer30-Processor-dev:*",
        "arn:aws:s3:::codepipeline-us-west-2-*",
        "arn:aws:codebuild:us-west-2:425480257575:report-group/Answer30-Processor-dev-*",
      ],
      actions: codeBuildPermissions,
    }));
  }
}