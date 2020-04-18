CREATE TABLE [dbo].[CustomerRequirementStep] (
    [Id]                    INT            NOT NULL,
    [CustomerRequirementId] INT            NOT NULL,
    [ObjectId]              NVARCHAR (50)  NOT NULL,
    [Process]               NVARCHAR (MAX) NULL,
    [Step]                  INT            NULL
);

