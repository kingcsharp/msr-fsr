CREATE TABLE [dbo].[A_ACTUAL_PARTS_HISTORY] (
    [ID]                     VARCHAR (50)    NOT NULL,
    [LOCATION]               VARCHAR (50)    NULL,
    [OBJECT_ID]              VARCHAR (50)    NULL,
    [DRCM]                   DATETIME        NULL,
    [MODBY]                  VARCHAR (50)    NULL,
    [NICK_NAME]              NVARCHAR (200)  NULL,
    [MERGABLE]               CHAR (1)        NULL,
    [PARENT_ID]              VARCHAR (50)    NULL,
    [PART_ID]                VARCHAR (50)    NULL,
    [QTY]                    FLOAT (53)      NULL,
    [SERIAL]                 VARCHAR (100)   NULL,
    [CUR_OWNER]              VARCHAR (50)    NULL,
    [ASSEMBLY_WT]            FLOAT (53)      NULL,
    [AP_STATUS]              VARCHAR (50)    NULL,
    [ROOT_ID]                VARCHAR (50)    NULL,
    [ROOT_STATUS]            VARCHAR (50)    NULL,
    [MODIFIED]               SMALLINT        NULL,
    [SYS_NAME]               NVARCHAR (2000) NULL,
    [PREV_OWNER]             VARCHAR (50)    NULL,
    [SUB_PART_ACTION]        VARCHAR (10)    NULL,
    [HAS_CHILD]              TINYINT         NULL,
    [RESPONSIBLE_PERSON]     VARCHAR (50)    NULL,
    [CHILD_PERCENT_COMPLETE] FLOAT (53)      NULL,
    CONSTRAINT [PK_A_ACTUAL_PARTS_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO



CREATE              TRIGGER A_ACTUAL_PARTS_HISTORY_INSERT
ON dbo.A_ACTUAL_PARTS_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50),@NAME as nvarchar(2000),@MODBY as nvarchar(50),
@ROOT_SUB_PART varchar(50),@partID varchar(50)



SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = isNULL(NICK_NAME,'') + isNULL('(' + SERIAL + ')',''),
	@partID = PART_ID 
FROM INSERTED
print 'my ID = ' + isNULL(@ID,'NULL')
SELECT @NAME = @NAME + ISNULL(NAME,'') + ISNULL('(' + COMPANY_PART_NUMBER + ')','') FROM A_V_PARTS_APPROVED_DATA WHERE ID = @partID

exec A_SP_OBJECT_ADD 'A_ACTUAL_PARTS_HISTORY',@ID,@NAME,@MODBY,null,null,null




GO










CREATE               TRIGGER A_ACTUAL_PARTS_HISTORY_UPDATE
ON dbo.A_ACTUAL_PARTS_HISTORY
AFTER UPDATE
AS
declare @OBJ_ID as nvarchar(50)
declare @ID as nvarchar(50)
declare @NAME as nvarchar(2000)
declare @MODBY as nvarchar(50)
declare @partID as nvarchar(50)
print 'In the Actual PArts History Update Trigger'
SELECT
@ID = ID,
@OBJ_ID = OBJECT_ID,
--@NAME =  + isNULL(NICK_NAME,'') + isNULL('(' + SERIAL + ')',''),
@MODBY = MODBY,
@partID = PART_ID
FROM INSERTED
print 'my ID = ' + isNULL(@ID,'NULL')
SELECT @NAME = dbo.A_FN_ACTUAL_PART_GET_NAME_FROM_HIST_ID_OR_ID (null,@ID)
print '^^^^^^^NAME = ' + @NAME
declare @myObjName as nvarchar(1000)
SELECT @myObjName = OBJ_DESC FROM A_OBJECTS WHERE ID = @OBJ_ID
print 'Checking OBj name verse new name'
if @myObjName <> @NAME
	begin
	print 'Obj NAme is not the same'
	UPDATE A_OBJECTS SET
	OBJ_DESC = @NAME,
	MODBY = @MODBY,
	DRCM = getDATE()
	WHERE ID = @OBJ_ID
	UPDATE A_ACTUAL_PARTS_HISTORY SET SYS_NAME = @NAME WHERE ID = @ID
	end

declare @rootID varchar(50)
SELECT @rootID = ROOT FROM A_OBJECTS WHERE ID = @OBJ_ID

declare @oldLoc varchar(50), @newLoc varchar(50),@oldPartID varchar(50),@newPartID varchar(50)
SELECT @newLoc = LOCATION,@newPartID = PART_ID FROM INSERTED
SELECT @oldLoc = LOCATION,@oldPartID = PART_ID FROM DELETED
print 'New Loc = ' + @newLoc
print 'New PID = ' + @newPartID
print 'Old Loc = ' + @oldLoc
print 'Old PID = ' + @oldPartID
exec A_SP_PART_SAFETY_STOCK_LEVEL_UPDATE_FOR_LOCATION_AND_PART @newLoc,@newPartID
exec A_SP_PART_SAFETY_STOCK_LEVEL_UPDATE_FOR_LOCATION_AND_PART @oldLoc,@oldPartID
print 'Out of the  Actual PArts History Update Trigger'




