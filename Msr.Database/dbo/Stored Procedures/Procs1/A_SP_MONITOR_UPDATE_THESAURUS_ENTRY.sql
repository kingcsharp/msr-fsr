



CREATE PROCEDURE dbo.A_SP_MONITOR_UPDATE_THESAURUS_ENTRY
@retVal varchar(50) OUTPUT,
@retMSG varchar(50) OUTPUT,
@rollUpID varchar(50),
@myText varchar(2000),
@description varchar(5000),
@syns varchar(5000),
@isFirst varchar(10),
@strNTLogin varchar(50)
AS
if @isFirst is not null 
	DELETE FROM A_MONITOR_TEXT_THESAURUS 
	WHERE MON_ROLL_UP_ID = @rollUpID
		AND  MAIN_TEXT = @myText
		AND SIMILAR_TEXT <> @myText

declare @myRoot varchar(50)
SELECT @myRoot = ID 
	FROM A_MONITOR_TEXT_THESAURUS 
	WHERE MON_ROLL_UP_ID = @rollUpID AND MAIN_TEXT = @myText AND SIMILAR_TEXT = @myText


declare @simList varchar(8000),@sql varchar(2000),@bigList varchar(8000)

set @bigList = @myRoot + ',' + @syns

CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @syns,','

declare @simText varchar(50),@simDesc varchar(2000)
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	SELECT @simText = MAIN_TEXT,@simDesc = DESCRIPTION FROM A_MONITOR_TEXT_THESAURUS WHERE ID = @it

	

	IF (NOT (EXISTS 
			(
			SELECT * FROM A_MONITOR_TEXT_THESAURUS 
			WHERE 
				MON_ROLL_UP_ID = @rollUpID AND 
				MAIN_TEXT = @myText AND 
				SIMILAR_TEXT = @simText
			)))
		begin

			print 'Main Text = ' + @myText
			print 'Sim Text = ' + @simText


			INSERT INTO A_MONITOR_TEXT_THESAURUS (ID,MON_ROLL_UP_ID,MAIN_TEXT,SIMILAR_TEXT)
			VALUES (newID(),@rollUpID,@myText,@simText)
			print '		exec A_SP_MONITOR_UPDATE_THESAURUS_ENTRY null,null,' +
			'''' + isNull(@rollUpID,'null') + ''',' +
			'''' + isNull(@simText,'null') + ''',' + 
			'''' + isNull(@simDesc,'null') + ''',' + 
			'''' + isNull(@myRoot,'null') + ''',' + 
			'''' + isNull(@strNTLogin,'null') + ''''
			
			

			set @sql = 'SELECT ID FROM A_MONITOR_TEXT_THESAURUS m WHERE m.MON_ROLL_UP_ID = ''' + @rollUpID + ''' 
			AND m.MAIN_TEXT = m.SIMILAR_TEXT AND
			m.MAIN_TEXT IN (SELECT SIMILAR_TEXT FROM A_MONITOR_TEXT_THESAURUS WHERE MON_ROLL_UP_ID = ''' + @rollUpID + '''
			AND MAIN_TEXT = ''' + @simText + ''')'
			print @sql
			exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@simList OUTPUT
			print @simList
			exec A_SP_MONITOR_UPDATE_THESAURUS_ENTRY
				@retVal OUTPUT,@retMSG OUTPUT,@rollUpID,@myText,@Description,@simList,null,@strNTLogin
			exec A_SP_MONITOR_UPDATE_THESAURUS_ENTRY
				@retVal OUTPUT,@retMSG OUTPUT,@rollUpID,@simText,@simDesc,@bigList,null,@strNTLogin




		end
Fetch Next from @curs Into @it


End
close @curs
Deallocate @curs








UPDATE A_MONITOR_TEXT_THESAURUS SET DESCRIPTION = @description WHERE 
	MAIN_TEXT = @myText AND SIMILAR_TEXT = @myText


fin:
