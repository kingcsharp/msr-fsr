






CREATE FUNCTION dbo.A_FN_QUOTE_ITEM_GET_PRECEDENT_LIST(@ID varchar(50))
RETURNS varchar(8000)
AS
BEGIN
declare @so as varchar(8000)
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT PREV FROM A_QUOTE_ITEM_PRECEDENTS WHERE FOL = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @so = isnull(@so + ',','') + @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
return(@so)
END









