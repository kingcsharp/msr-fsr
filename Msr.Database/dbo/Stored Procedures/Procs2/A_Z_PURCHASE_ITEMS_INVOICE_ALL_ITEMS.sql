
CREATE  PROCEDURE dbo.A_Z_PURCHASE_ITEMS_INVOICE_ALL_ITEMS
AS

Declare @it nvarchar(50),@curs Cursor,@fit varchar(50)

print 'First Lets update all Fills so the trigger fixes everything and insures integrity'
set @curs = Cursor For SELECT ID FROM A_FILLS
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	UPDATE A_FILLS SET FILL_OBJ_ID = FILL_OBJ_ID WHERE ID = @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Now Lets start invoicing'

set @curs = Cursor For 
SELECT o.FILL_ITEM_ID,o.PURCHASE_ITEM_ID FROM 
	A_TASK_ORDER_INFORMATION o, A_TASKS t
WHERE 
	t.ID = o.TASK_ID
	and o.FILL_ITEM_ID IS NOT NULL 
	AND o.PURCHASE_ITEM_ID IS NOT NULL 
	 AND	t.STATUS in ('FINISHED','CLOSED','COMPLETED') 
open @curs
Fetch Next from @curs Into @fit,@it
while (@@fetch_status = 0)
Begin
	print 'looking at task = ' + @it
	exec A_SP_PURCHASE_ITEM_INVOICE_ITEM @it,@fit,'7'
	Fetch Next from @curs Into @fit,@it
End
close @curs
Deallocate @curs

-- set @curs = Cursor For SELECT ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID IS NOT NULL ORDER BY DRCM DESC
-- open @curs
-- Fetch Next from @curs Into @it
-- while (@@fetch_status = 0)
-- Begin
-- 	print 'going to invoice the item = ' + @it
-- 	exec A_SP_PURCHASE_ITEM_INVOICE_ITEM @it,'SYSTEM'
-- 	Fetch Next from @curs Into @it
-- End
-- close @curs
-- Deallocate @curs
 



