
CREATE  procedure  dbo.A_SP_TASK_FINISH_CHECK_ORDER_QUOTE_DATA
@errMsg varchar(100) OUTPUT,
@taskID varchar(50)
AS
declare @qoLinkID varchar(50)
SELECT @qoLinkID = ID FROM A_QUOTE_ORDER_LINK WHERE TASK_ID = @taskID
if @qoLinkID is Null goto success

declare @quoteID varchar(50),@quoteStatus varchar(50)
SELECT @quoteID = QUOTE_ID FROM A_QUOTE_ORDER_LINK WHERE ID = @qoLinkID
SELECT @quoteStatus = STATUS FROM A_OBJECTS WHERE OBJ_ID = @quoteID
if @quoteStatus <> 'APPROVED'
	begin
	set @errMsg = 'This task will close on it''s own once the quote is approved.'
	goto problem
	end


success:
return 0

problem:
return 1


