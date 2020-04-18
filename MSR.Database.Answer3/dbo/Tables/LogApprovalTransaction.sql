CREATE TABLE [dbo].[LogApprovalTransaction] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [ApprovalEntity]   VARCHAR (20) NOT NULL,
    [ApprovalEntityId] INT          NOT NULL,
    [ApprovedById]     INT          NOT NULL,
    [ApprovedOn]       DATETIME     NOT NULL,
    CONSTRAINT [PK_Log_ApprovalTransaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LogApprovalTransaction_ApprovedBy] FOREIGN KEY ([ApprovedById]) REFERENCES [dbo].[User] ([Id])
);

