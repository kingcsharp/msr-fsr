import * as cdk from "aws-cdk-lib";
import { Match, Template } from "aws-cdk-lib/assertions";
import * as Cdk from "../lib/answer30-stack";


test("IAM Role Created", () => {
    const app = new cdk.App();
    const stack = new Cdk.Answer30Stack(app, "TestAnswer30Stack");
    const template = Template.fromStack(stack);
    template.hasResourceProperties(
        "AWS::IAM::Role",
        Match.objectEquals({
            AssumeRolePolicyDocument: {
                Version: "2012-10-17",
                Statement: [
                    {
                        Action: "sts:AssumeRole",
                        Effect: "Allow",
                        Principal: {
                            Service: "codebuild.amazonaws.com"
                        },
                    },
                ],
            },
        })
    );
});
