CREATE TABLE [dbo].[Portal_PartInfo] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ObjectId]        NVARCHAR (50)  NULL,
    [PartDescription] NVARCHAR (100) NULL,
    [Substrate]       NVARCHAR (50)  NULL,
    [CoatingSurface]  NVARCHAR (50)  NULL,
    [CustPartNo]      NVARCHAR (50)  NULL,
    [MfgPartNo]       NVARCHAR (50)  NULL,
    [PartsPerKit]     NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Portal_PartInfo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Portal_PartInfo_Portal_CustomerRequirement] FOREIGN KEY ([ObjectId]) REFERENCES [dbo].[Portal_CustomerRequirement] ([Id])
);





