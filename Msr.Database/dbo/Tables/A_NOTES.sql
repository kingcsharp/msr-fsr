CREATE TABLE [dbo].[A_NOTES] (
    [ID]               VARCHAR (50)    NOT NULL,
    [TXT]              NVARCHAR (4000) NULL,
    [DRCM]             DATETIME        NULL,
    [MODBY]            VARCHAR (50)    NULL,
    [PEOPLE_SECURE]    TINYINT         NULL,
    [COMPANY_SECURE]   TINYINT         NULL,
    [ROLE_SECURE]      TINYINT         NULL,
    [SECURITY_LEVEL]   VARCHAR (50)    NULL,
    [NOTE_TYPE]        VARCHAR (50)    NULL,
    [SECURITY_TYPE]    VARCHAR (50)    NULL,
    [RESPONSE_ALLOWED] VARCHAR (50)    NULL,
    [STATUS]           VARCHAR (50)    NULL,
    [AUTHOR]           VARCHAR (50)    NULL,
    [REVISION]         INT             NULL,
    [TO_READ_COUNT]    INT             NULL,
    [DATE_CREATED]     DATETIME        NULL,
    [HIDE_NOTE]        TINYINT         NULL,
    [NOTIFY]           TINYINT         NULL,
    [DATE_SENT]        DATETIME        NULL,
    CONSTRAINT [PK_A_NOTES] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE       TRIGGER [dbo].[A_NOTES_INSERT_UPDATE]
ON [dbo].[A_NOTES]
AFTER INSERT, UPDATE
AS
declare @ID as nvarchar(50)
declare @TXT as nvarchar(50)
declare @MODBY as nvarchar(50)
declare @rev as int
SELECT @ID = ID,
	@MODBY = MODBY,
	@TXT = TXT FROM INSERTED
if not exists(SELECT * FROM A_NOTE_TEXT_HISTORY WHERE NOTE_ID = @ID AND TXT = @TXT)
	begin
	SELECT @rev = max(REVISION) + 1 FROM A_NOTE_TEXT_HISTORY WHERE NOTE_ID = @ID
	INSERT INTO A_NOTE_TEXT_HISTORY (ID,NOTE_ID,TXT,DRCM,MODBY,REVISION)
		VALUES (newID(),@ID,@TXT,getDate(),@MODBY,@rev)
	end



