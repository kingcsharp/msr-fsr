


CREATE      PROCEDURE dbo.A_SP_ADMIN_PROCEDURE_SAVE_NEWS_PROCEDURE
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@procs varchar(4000),
@strNTLogin varchar(50)
AS
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

print 'Updating News Document'
print 'Delete the old ones'
DELETE FROM A_ADMIN_PROCEDURES WHERE PROC_TYPE = 1 AND COMPANY = @myCO
print 'insert the new ones'
Declare @it nvarchar(50),@cnt int
Declare @curs Cursor
CREATE TABLE #TempItems(ID varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @procs,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
set @cnt = 1
while (@@fetch_status = 0)
Begin
	print 'Adding Proc ' + @it
	INSERT INTO A_ADMIN_PROCEDURES (ID,COMPANY,PROCEDURE_ID,PROC_TYPE,
		DRCM,MODBY,PRINT_ORDER)
		VALUES (newID(),@myCO,@it,'1',getDate(),@strNTLogin,@cnt)
	Fetch Next from @curs Into @it
	set @cnt = @cnt + 1
End
close @curs
Deallocate @curs

