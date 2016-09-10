

CREATE   PROCEDURE dbo.A_SP_ORDER_ITEM_DELETE
@id varchar(50),
@strNTLogin varchar(50)
AS
print 'The ID is ' + @ID
declare @pID as varchar(50), @objID as varchar(50), @quoteID varchar(50), @orderID varchar(50), @res int
SELECT @quoteID = QUOTE_ID, @orderID = ORDER_ID FROM A_ORDER_ITEMS WHERE ID = @ID
if @quoteID is not null SELECT @objID = OBJECT_ID FROM A_QUOTES_HISTORY WHERE ID = @quoteID
else SELECT @objID = OBJECT_ID FROM A_ORDERS_HISTORY WHERE ID = @orderID
print 'ObjID = ' + isNull(@objID,'NULL')
print 'First verify this person is editing this object'
declare @tester as varchar(50)
if not(exists(SELECT ID FROM A_OBJECTS WHERE ID = @objID AND LOCKED_BY = @strNTLogin)) goto problem

print 'Now it is time to get the parent data before we delete this one'
SELECT @pID = PARENT FROM A_ORDER_ITEMS WHERE ID = @id

print 'Now lets call delete for all my children'
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_ORDER_ITEMS WHERE PARENT = @id
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	exec @res = A_SP_ORDER_ITEM_DELETE @it,@strNTLogin
	print 'back'
	if @res = 1 goto problem2
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Now Delete this one'
DELETE FROM A_ORDER_ITEM_PRECEDENTS WHERE FOL = @id or PREV = @id
DELETE FROM A_ORDER_ITEMS WHERE ID = @id

exec A_SP_ORDER_ITEM_SET_PRICE @pID,@strNTLogin



fin:
return 0
problem:
print 'There was a problem'
return 1
problem2:
print 'There was a problem2'
return 1


