



CREATE    PROCEDURE DBO.A_SP_ACTUAL_PARTS_CREATE_CHILD_LIST
@ret varchar(8000) OUTPUT,
@ID varchar(50),
@lev smallInt,
@rootPartQty float,
@strNTLogin varchar(50)
AS
if @rootPartQty is null SELECT @rootPartQty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @ID

set @lev = @lev + 1
declare @myPartID varchar(50),@myQty varchar(50)
SELECT @myPartID = PART_ID,@myQty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @ID
print 'lev = '
print @lev
if ((@lev + 0)> 1)
	begin
		set @ret = isNull(@ret + ',','') + convert(varchar(10),@lev) + '-' + isNull(@myPartID,'NULL') + isNull('('+convert(varchar(50),(@myQty / @rootPartQty))+')','NULL')
	end
else 
	begin
		set @ret = isNull(@ret + ',','') + convert(varchar(10),@lev) + '-' + @myPartID
	end

print 'Creating a list for part ' + @ID
Declare @partID as varchar(50),@childID varchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT PART_ID,ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE PARENT_ID = @ID ORDER BY PART_ID
open @curs
Fetch Next from @curs Into @partID,@childID
while (@@fetch_status = 0)
Begin
	exec A_SP_ACTUAL_PARTS_CREATE_CHILD_LIST @ret OUTPUT,@childID,@lev,@rootPartQty,@strNTLogin
	Fetch Next from @curs Into @partID,@childID
End
close @curs
Deallocate @curs 
declare @myPID as varchar(50)



