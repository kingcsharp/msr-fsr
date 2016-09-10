CREATE TABLE [dbo].[A_OBJECTS] (
    [ID]                VARCHAR (50)    NOT NULL,
    [OBJ_TABLE]         VARCHAR (50)    NOT NULL,
    [OBJ_ID]            VARCHAR (50)    NOT NULL,
    [OBJ_DESC]          NVARCHAR (2000) NULL,
    [DRCM]              DATETIME        NULL,
    [MODBY]             VARCHAR (50)    NULL,
    [CO_PART_NUM]       VARCHAR (50)    NULL,
    [PART_TYPE]         VARCHAR (50)    NULL,
    [PART_CO]           VARCHAR (50)    NULL,
    [LOCKED_BY]         VARCHAR (50)    NULL,
    [UNLOCKED_BY]       VARCHAR (50)    NULL,
    [CREATED_BY]        VARCHAR (50)    NULL,
    [CREATE_DATE]       DATETIME        NULL,
    [ROOT]              VARCHAR (50)    NULL,
    [REV_INFO]          NVARCHAR (500)  NULL,
    [CREATING_CO]       VARCHAR (50)    NULL,
    [STATUS]            VARCHAR (50)    NULL,
    [REV]               INT             NULL,
    [WFS_ID]            VARCHAR (50)    NULL,
    [LOCKED_BY_NAME]    NVARCHAR (50)   NULL,
    [CREATING_CO_NAME]  NVARCHAR (50)   NULL,
    [APPROVAL_ACTIVITY] VARCHAR (50)    NULL,
    [APPROVAL_DATE]     DATETIME        NULL,
    CONSTRAINT [PK_A_OBJECTS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE    TRIGGER A_OBJECT_INSERT
ON dbo.A_OBJECTS
AFTER INSERT
AS
print 'IN A_OBJECT_INSERT Trigger'
declare @objID as nvarchar(50)
declare @modBY as nvarchar(50)
declare @myTable as nvarchar(50)
declare @myID as nvarchar(50)
SELECT @objID = ID,@modBy = modby,@myTable = OBJ_TABLE,@myID = OBJ_ID FROM INSERTED
print 'Calling to make it creating'
exec A_SP_OBJECT_MAKE_CREATING @objID,@modBY
declare @sql as nvarchar(1000)
set @sql = 'UPDATE ' + @myTable + ' SET OBJECT_ID = ''' + @objID + ''' WHERE ID = ''' + @myID + ''''
print @sql
exec(@sql)

print 'Out of A_OBJECT_INSERT Trigger'


GO






CREATE                  TRIGGER A_OBJECT_UPDATE
ON dbo.A_OBJECTS
AFTER UPDATE
AS
declare @ID as nvarchar(50)
declare @LOCKED_BY as nvarchar(50)
declare @STAT as nvarchar(50)
declare @ROOT as nvarchar(50)
declare @MODBY as nvarchar(50),@objTable varchar(50)
print 'Entering the trigger A_OBJECT_UPDATE'
SELECT
@ID = ID,
@LOCKED_BY = LOCKED_BY,
@STAT = STATUS,
@ROOT = ROOT,
@MODBY = MODBY,
@objTable = OBJ_TABLE
FROM INSERTED

declare @curLockedBy as nvarchar(100)
--Set The Name of the Locked By in here
IF @LOCKED_BY IS NULL
	begin
		print 'LOCKED_BY is null so I need to set the name to null as well.'
		SELECT @curLockedBy = LOCKED_BY_NAME FROM A_OBJECTS where ID = @ID
		if @curLockedBy is not null
			begin
			UPDATE A_OBJECTS SET LOCKED_BY_NAME = NULL WHERE ID = @ID
			end
	end
else
	begin
		declare @pNAME as nvarchar(200)
		SELECT @pNAME = FULL_NAME FROM A_PEOPLE_HISTORY h,A_PEOPLE p WHERE h.ID = p.HISTORY_REF_ID AND p.ID = @LOCKED_BY
		print 'Locked By = ' + @LOCKED_BY + ' with name = ' + @pNAME
		SELECT @curLockedBy = LOCKED_BY_NAME FROM A_OBJECTS where ID = @ID
		if @curLockedBy <> @pName
			begin
			UPDATE A_OBJECTS SET LOCKED_BY_NAME = @pNAME WHERE ID = @ID
			end
	
	end
print 'Done updating the name'

--If this is an approved object which there should only be one of we need to make sure it is in the
--A_APPROVED_OBJECTS table
if @STAT like 'APPROVED%'
	begin
		declare @t as nvarchar(50)
		SELECT @t = ID FROM A_APPROVED_OBJECTS WHERE ID = @ROOT
		IF @t is null
			INSERT INTO A_APPROVED_OBJECTS (ID,OBJ_REF_ID,DRCM,MODBY) VALUES (@ROOT,@ID,getDate(),@MODBY)
		else
			update A_APPROVED_OBJECTS SET
				OBJ_REF_ID = @ID,
				DRCM = getDate()
			WHERE ID = @ROOT
	end



print 'Exiting the trigger A_OBJECT_UPDATE'






