CREATE TABLE [dbo].[TimeZone] (
    [Id]                NVARCHAR (50)  NOT NULL,
    [Description]       NVARCHAR (100) NULL,
    [Offset]            REAL           NULL,
    [Number]            INT            NULL,
    [UseDalightSavings] SMALLINT       NULL
);

