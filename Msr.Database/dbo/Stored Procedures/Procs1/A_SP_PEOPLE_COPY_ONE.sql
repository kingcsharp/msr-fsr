






CREATE          procedure A_SP_PEOPLE_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@copyPrefix nvarchar(50),
	@strNTLogin nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_PEOPLE_HISTORY (ID,LOGIN,NAME,PASSWORD,BOSS,SOURCE,LAST_NAME,MIDDLE_NAME,
NICK_NAME,LANG,HIRE_DATE,DRCM,MODBY,COMPANY,TIME_ZONE,SYSTEM_STATUS,
CO_POSITION,ROOT_COMPANY,TOOL_BOX,SCREEN_TYPE)
SELECT @newID as ID,LOGIN,NAME,PASSWORD,BOSS,'A_SP_PEOPLE_COPY_ONE',LAST_NAME,MIDDLE_NAME,
NICK_NAME,LANG,HIRE_DATE,getDate(),@strNTLogin,COMPANY,TIME_ZONE,SYSTEM_STATUS,
CO_POSITION,ROOT_COMPANY,TOOL_BOX,SCREEN_TYPE FROM A_PEOPLE_HISTORY WHERE ID = @strID

--if it is a copy then change the name
if len(@copyPrefix) > 0
	begin
	UPDATE A_PEOPLE_HISTORY SET
	NAME = @copyPrefix + NAME,
	PASSWORD = NULL,
	LOGIN = NULL
	WHERE
	ID = @newID
	end

--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_PEOPLE_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID








