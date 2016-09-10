






CREATE     PROCEDURE A_SP_PEOPLE_GET_EMAILS_FROM_LIST
	@em1 varchar(8000) OUTPUT,
	@em2 varchar(8000) OUTPUT,
	@em3 varchar(8000) OUTPUT,
	@strList varchar(8000),
	@strNTLogin nvarchar(50)
AS
set @em1 = ''
set @em2 = ''
set @em3 = ''

CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @strList,','
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
declare @email as varchar(300)
while (@@fetch_status = 0)
	Begin
	print 'Looking up email for ' + @it
	set @email = ''
	SELECT @email = isNULL(ADDY + '---' + @it + ';','') FROM A_V_EMAILS_FOR_APPROVED_PEOPLE 
	WHERE ID = @it  AND [EMAIL_TYPE] = 'SYS-EMAIL-1'
	print 'Email = '
	print @email
	if len(@em1 + @email) > 8000
		begin
		if len(@em2 + @email) > 8000
			begin
			set @em3 = @em3 + @email
			end
		else
			set @em2 = @em2 + @email
		end
	else
		set @em1 = @em1 + @email
	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs






