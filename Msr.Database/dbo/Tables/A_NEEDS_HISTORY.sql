CREATE TABLE [dbo].[A_NEEDS_HISTORY] (
    [ID]                     VARCHAR (50)    NOT NULL,
    [OBJECT_ID]              VARCHAR (50)    NULL,
    [DESCRIPTION]            NVARCHAR (4000) NULL,
    [CUSTOMER_CO]            VARCHAR (50)    NULL,
    [TIMEFRAME]              VARCHAR (50)    NULL,
    [IMPORTANCE]             VARCHAR (50)    NULL,
    [NEED_SIZE]              VARCHAR (50)    NULL,
    [ADVERTISING_START_DATE] DATETIME        NULL,
    [ADVERTISING_STOP_DATE]  DATETIME        NULL,
    [ADVERSTISE_PUBLICLY]    VARCHAR (50)    NULL,
    [CONTACT_NAME]           VARCHAR (50)    NULL,
    [CONTACT_EMAIL]          VARCHAR (50)    NULL,
    [CONTACT_PHONE]          VARCHAR (50)    NULL,
    [DRCM]                   DATETIME        NULL,
    [MODBY]                  VARCHAR (50)    NULL,
    CONSTRAINT [PK_A_NEEDS_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE           TRIGGER A_NEEDS_HISTORY_INSERT
ON dbo.A_NEEDS_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = DESCRIPTION FROM INSERTED
exec A_SP_OBJECT_ADD 'A_NEEDS_HISTORY',@ID,@NAME,@MODBY,null,null,null


GO

CREATE    TRIGGER A_NEEDS_HISTORY_UPDATE
ON dbo.A_NEEDS_HISTORY
AFTER UPDATE
AS
declare @OBJ_ID as nvarchar(50)
declare @ID as nvarchar(50)
declare @NAME as nvarchar(1000)
declare @MODBY as nvarchar(50)
print 'In the Needs History Update Trigger'
SELECT
@ID = ID,
@OBJ_ID = OBJECT_ID,
@NAME = DESCRIPTION,
@MODBY = MODBY 
FROM INSERTED

declare @myObjName as nvarchar(100)
SELECT @myObjName = OBJ_DESC FROM A_OBJECTS WHERE ID = @OBJ_ID
print 'My Obj Desc = ' + isNull(@myObjName,'NULL')
if @myObjName <> @NAME
	begin
	UPDATE A_OBJECTS SET
	OBJ_DESC = @NAME,
	MODBY = @MODBY,
	DRCM = getDATE()
	WHERE ID = @OBJ_ID
	end




