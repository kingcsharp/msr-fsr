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
GO

CREATE TRIGGER [dbo].[Portal_Actual_Part_Insert_SubPart]
ON [dbo].[A_ACTUAL_PARTS_HISTORY]
AFTER update
AS
declare @ID as nvarchar(50),@NICK_NAME as nvarchar(2000),@MODBY as nvarchar(50),@newID varchar(50),
@ROOT_SUB_PART varchar(50),@partID varchar(50),@Serial nvarchar(100),@strNTLogin varchar(50),@PARENT_ID varchar(100),@QTY int,
@count int,@SysName nvarchar(500),@CurOwner varchar(50),@NAME nvarchar(100),@ApStatus nvarchar(50),@myRoot varchar(50),@rev int,@AID varchar(50),@flag int,
@OldQty int


SET @count=1;

SELECT 
	@MODBY = MODBY,
	@NICK_NAME =NICK_NAME,
	@Serial=  SERIAL,
	@partID = PART_ID ,
	@strNTLogin=MODBY,
	@PARENT_ID=PARENT_ID,
	@QTY=QTY,
	@SysName=SYS_NAME,
	@CurOwner=CUR_OWNER,
	@ApStatus=AP_STATUS,
	@PARENT_ID=PARENT_ID
FROM INSERTED


IF @QTY>@count
BEGIN

SET @OldQty=@QTY

 SET @flag=(SELECT max(CountValue) FROM Portal_AddSubPartQtyCount WHERE ParentId=@PARENT_ID AND Qty=@QTY AND PartId=@partID)

 IF @flag IS NULL

 BEGIN
   SET @flag=1;
 END
  
   WHILE @QTY >@flag AND  @Serial is NULL

   BEGIN 
   SET @flag=null;
   SET @count=@count+1;

    exec sp_GetUniqueID3 @newID OUTPUT
  
  INSERT INTO Portal_AddSubPartQtyCount (CountValue,ParentId,Qty,ActualPartId,PartId) 
  VALUES(@count, @PARENT_ID,@OldQty,@newID,@partID)

  SET @flag=(SELECT max(CountValue) FROM Portal_AddSubPartQtyCount WHERE ParentId=@PARENT_ID )

   INSERT INTO A_ACTUAL_PARTS_HISTORY (ID,NICK_NAME,SERIAL,MODBY,DRCM,PARENT_ID) 
			values(@newID,@NICK_NAME,@SERIAL,@strNTLogin,getDATE(),@PARENT_ID)

	SELECT @NAME = dbo.A_FN_ACTUAL_PART_GET_NAME_FROM_HIST_ID_OR_ID (null,@newID)

	UPDATE A_ACTUAL_PARTS_HISTORY SET QTY=1,SYS_NAME = @NAME,PART_ID=@partID,CUR_OWNER=@CurOwner,AP_STATUS=@ApStatus WHERE ID=@newID

	UPDATE A_OBJECTS SET STATUS = 'APPROVED',LOCKED_BY = NULL WHERE A_OBJECTS.OBJ_ID  = @newID

	SELECT @myRoot = ROOT,@ID = OBJ_ID,@rev = REV FROM A_OBJECTS WHERE OBJ_ID = @newID

	declare @tester as nvarchar(50) --Test to see if it is in the table
    SELECT @tester = ID FROM A_ACTUAL_PARTS WHERE ID = @myRoot 

	if @tester is Null --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_ACTUAL_PARTS(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@ID,getDate(),@strNTLogin)
	END
	
	UPDATE A_ACTUAL_PARTS SET STATUS = 'APPROVED' WHERE ID = @myRoot

END
END
GO

