CREATE TABLE [dbo].[Portal_ProcessInfo] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [ObjectId]         NVARCHAR (50) NULL,
    [Contaminents]     NVARCHAR (50) NULL,
    [NonCU]            NVARCHAR (50) NULL,
    [Material]         NVARCHAR (50) NULL,
    [ApproxDimensions] NVARCHAR (50) NULL,
    [ExistingProcess]  NVARCHAR (50) NULL,
    CONSTRAINT [PK_Portal_ProcessInfo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Portal_ProcessInfo_Portal_CustomerRequirement] FOREIGN KEY ([ObjectId]) REFERENCES [dbo].[Portal_CustomerRequirement] ([Id])
);

