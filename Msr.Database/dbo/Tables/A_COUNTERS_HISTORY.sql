CREATE TABLE [dbo].[A_COUNTERS_HISTORY] (
    [ID]             VARCHAR (50)   NOT NULL,
    [NAME]           VARCHAR (50)   NULL,
    [DRCM]           DATETIME       NULL,
    [MODBY]          VARCHAR (50)   NULL,
    [OBJECT_ID]      VARCHAR (50)   NULL,
    [CUR_VAL]        FLOAT (53)     NULL,
    [DT_RECORDED]    DATETIME       NULL,
    [REL_OBJECT_ID]  VARCHAR (50)   NULL,
    [REL_OBJEC_NAME] VARCHAR (2000) NULL,
    CONSTRAINT [PK_A_COUNTERS_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE        TRIGGER A_COUNTERS_HISTORY_INSERT
ON dbo.A_COUNTERS_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = NAME FROM INSERTED
exec A_SP_OBJECT_ADD 'A_COUNTERS_HISTORY',@ID,@NAME,@MODBY,null,null,null


GO
CREATE          TRIGGER A_COUNTERS_HISTORY_UPDATE
ON dbo.A_COUNTERS_HISTORY
AFTER UPDATE
AS
declare @OBJ_ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
print 'In the Counters History Update Trigger'
if update(NAME)
	begin
	SELECT
	@OBJ_ID = OBJECT_ID,
	@NAME = NAME,
	@MODBY = MODBY 
	FROM INSERTED

	if exists(SELECT * FROM A_OBJECTS WHERE ID = @OBJ_ID AND OBJ_DESC <> @NAME) 
		begin
		print 'The name has changed'
		UPDATE A_OBJECTS SET
			OBJ_DESC = @NAME,
			MODBY = @MODBY,
			DRCM = getDATE()
			WHERE ID = @OBJ_ID
		end
	end
