CREATE TABLE [dbo].[A_PEOPLE_HISTORY] (
    [ID]            VARCHAR (50)  NOT NULL,
    [LOGIN]         NVARCHAR (50) NULL,
    [NAME]          NVARCHAR (50) NOT NULL,
    [PASSWORD]      NVARCHAR (50) NULL,
    [BOSS]          NVARCHAR (50) NULL,
    [SOURCE]        NVARCHAR (50) NULL,
    [LAST_NAME]     NVARCHAR (50) NULL,
    [MIDDLE_NAME]   NVARCHAR (50) NULL,
    [NICK_NAME]     NVARCHAR (50) NULL,
    [LANG]          NVARCHAR (50) NOT NULL,
    [HIRE_DATE]     DATETIME      NULL,
    [DRCM]          DATETIME      NULL,
    [MODBY]         NVARCHAR (50) NULL,
    [OBJECT_ID]     VARCHAR (50)  NULL,
    [COMPANY]       VARCHAR (50)  NULL,
    [TIME_ZONE]     NVARCHAR (50) NULL,
    [FULL_NAME]     NVARCHAR (50) NULL,
    [SYSTEM_STATUS] VARCHAR (50)  NULL,
    [CO_POSITION]   VARCHAR (50)  NULL,
    [ROOT_COMPANY]  VARCHAR (50)  NULL,
    [IS_HEAD]       SMALLINT      NULL,
    [TOOL_BOX]      TINYINT       CONSTRAINT [DF_A_PEOPLE_HISTORY_TOOL_BOX] DEFAULT ((1)) NULL,
    [INFO_BOX]      TINYINT       CONSTRAINT [DF_A_PEOPLE_HISTORY_INFO_BOX] DEFAULT ((1)) NULL,
    [ADV_SEARCH]    TINYINT       CONSTRAINT [DF_A_PEOPLE_HISTORY_ADV_SEARCH] DEFAULT ((1)) NULL,
    [COLOR_KEY]     TINYINT       CONSTRAINT [DF_A_PEOPLE_HISTORY_COLOR_KEY] DEFAULT ((1)) NULL,
    [SCREEN_TYPE]   VARCHAR (50)  NULL,
    [CHANGE_PASS]   TINYINT       NULL,
    CONSTRAINT [PK_A_PEOPLE_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO



CREATE         TRIGGER A_PEOPLE_HISTORY_INSERT
ON dbo.A_PEOPLE_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = isNull(NAME,'') + ' ' + isNull(MIDDLE_NAME + ' ','') + isNull(LAST_NAME,'') FROM INSERTED
exec A_SP_OBJECT_ADD 'A_PEOPLE_HISTORY',@ID,@NAME,@MODBY,null,null,null
UPDATE A_PEOPLE_HISTORY SET FULL_NAME = @NAME WHERE ID = @ID

print 'Going to set the root Copany now for ID = ' + isNull(@ID,'NULL')
exec A_SP_PEOPLE_SET_ROOT_COMPANY @ID
print 'Root Company set'



GO


CREATE          TRIGGER A_PEOPLE_HISTORY_UPDATE
ON dbo.A_PEOPLE_HISTORY
AFTER UPDATE
AS

declare @OBJ_ID as nvarchar(50)
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
print 'In the Poeple History Update Trigger'
SELECT
@ID = ID,
@OBJ_ID = OBJECT_ID,
@NAME = isNull(NAME,'') + ' ' + isNull(MIDDLE_NAME + ' ','') + isNull(LAST_NAME,''),
@MODBY = MODBY 
FROM INSERTED

if update(COMPANY)
begin
	print 'Going to set the root Copany now for ID = ' + isNull(@ID,'NULL')
	exec A_SP_PEOPLE_SET_ROOT_COMPANY @ID
	print 'Root Company set'
end


if update(NAME) or update(MIDDLE_NAME) or update(LAST_NAME)
begin
UPDATE A_OBJECTS SET
OBJ_DESC = @NAME,
MODBY = @MODBY,
DRCM = getDATE()
WHERE ID = @OBJ_ID
UPDATE A_PEOPLE_HISTORY SET FULL_NAME = @NAME WHERE ID = @ID
end

exec A_SP_PEOPLE_UPDATE_PERSON_SEARCH_TABLE @OBJ_ID

declare @mySubs as cursor
Set @mySubs = Cursor for SELECT OBJECT_ID from A_PEOPLE_HISTORY WHERE BOSS = @ID
declare @subID nvarchar(50)
Open @mySubs
Fetch Next from @mySubs Into @subID
while (@@fetch_status = 0)
	begin
	exec A_SP_PEOPLE_UPDATE_PERSON_SEARCH_TABLE @subID
	Fetch Next from @mySubs Into @subID
	End



