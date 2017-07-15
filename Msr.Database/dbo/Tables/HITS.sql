CREATE TABLE [dbo].[HITS] (
    [PkId]      INT            IDENTITY (1, 1) NOT NULL,
    [ID]        NVARCHAR (255) NOT NULL,
    [LAST_USER] NVARCHAR (50)  NULL,
    [DATETIME]  DATETIME       NULL,
    [NUM]       NUMERIC (18)   NULL
);




GO
CREATE NONCLUSTERED INDEX [index_id]
    ON [dbo].[HITS]([ID] ASC);

