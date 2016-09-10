CREATE PROCEDURE DBO.A_SP_ACTUAL_PARTS_ADJUST_CHILD_QTYS 
@pID varchar(50),
@oldQty float,
@newQty float
AS
declare @curs CURSOR,@id varchar(50),@histID varchar(50),@cOldQty float,@cNewQty float
set @curs = Cursor For SELECT ID,HISTORY_REF_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE PARENT_ID = @pID
open @curs
Fetch Next from @curs Into @id,@histID
while (@@fetch_status = 0)
Begin
	SELECT @cOldQty = QTY FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @histID
	SET @cNewQty = @cOldQty * (@newQty/@oldQty)
	UPDATE A_ACTUAL_PARTS_HISTORY SET QTY = @cNewQty WHERE ID = @histID
	exec A_SP_ACTUAL_PARTS_ADJUST_CHILD_QTYS @ID,@cOldQty,@cNewQty
	Fetch Next from @curs Into @id,@histID
End
close @curs
Deallocate @curs
