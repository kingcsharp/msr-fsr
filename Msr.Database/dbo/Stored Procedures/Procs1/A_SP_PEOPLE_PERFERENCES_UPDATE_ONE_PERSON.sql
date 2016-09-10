








CREATE                  PROCEDURE A_SP_PEOPLE_PERFERENCES_UPDATE_ONE_PERSON
@ID varchar(50),
@LOGIN varchar(50),
@CHANGED_PASSWORD varchar(50),
@LANG varchar(50),
@TIME_ZONE varchar(50),
@BACKGROUND varchar(50),
@TOOL_BOX varchar(50),
@INFO_BOX varchar(50),
@ADV_SEARCH tinyint,
@COLOR_KEY tinyInt,
@SCREEN_TYPE varchar(20),
@strNTLogin varchar(50)
AS
declare @MESSAGES as nvarchar(100)
Declare @curs Cursor
Declare @it varchar(50)
print 'Updating the preferences '
set @curs = Cursor For 
SELECT  peopleHistID FROM A_V_PEOPLE_PERFERENCES_GET_HISTORY_ID 
	WHERE ROOT=@strNTLogin 
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
  	Begin
	if @CHANGED_PASSWORD is not null
        UPDATE A_PEOPLE_HISTORY SET PASSWORD= @CHANGED_PASSWORD, CHANGE_PASS = 0 WHERE ID = @it
	if @LOGIN is not null
		UPDATE A_PEOPLE_HISTORY SET	LOGIN = @LOGIN WHERE ID = @it
	UPDATE A_PEOPLE_HISTORY SET
		TIME_ZONE = @TIME_ZONE, 
		LANG = @LANG,
		TOOL_BOX = @TOOL_BOX,
		INFO_BOX = @INFO_BOX,
		ADV_SEARCH = @ADV_SEARCH,
		COLOR_KEY = @COLOR_KEY,
		MODBY = @strNTLogin,
		SCREEN_TYPE = @SCREEN_TYPE,
		DRCM = getDate()
		WHERE ID = @it
	Fetch Next from @curs Into @it
   End
close @curs
Deallocate @curs


DELETE FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @strNTLogin AND TYPE = 'BACKGROUND'
if @BACKGROUND IS NOT NULL
	INSERT INTO A_DOCUMENT_LINK (ID,LINKED_DOC_ID,OBJECT_ID,MODBY,DRCM,TYPE)
	VALUES (newID(),@BACKGROUND,@strNTLogin,@strNTLogin,getDate(),'BACKGROUND')







