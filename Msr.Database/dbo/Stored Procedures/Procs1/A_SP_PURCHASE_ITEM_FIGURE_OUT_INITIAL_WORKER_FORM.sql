CREATE PROCEDURE dbo.A_SP_PURCHASE_ITEM_FIGURE_OUT_INITIAL_WORKER_FORM
@purchItemID varchar(50)
AS
declare @procID varchar(50),@pQty float,@fillObjID varchar(50),@procHistID varchar(50)
SELECT @procID = PROC_ID, @pQty = PURCHASE_QTY, @fillObjID = FILL_OBJ_ID
	FROM A_V_FILLS_SEARCH PURCH_ITEM_ID 
	WHERE PURCH_ITEM_ID = @purchItemID
SELECT @procHistID = ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = @procID
print 'Procedure ID = ' + @procID
print 'fill obj ID = ' + @fillObjID
print 'Qty = '
print @pQty
if @pQty = 1
	begin
	print 'Since the Qty is 1 I can take them to the one part screen'	
	SELECT 'INDIVIDUAL_PART' AS INITIAL_SCREEN_TYPE, @fillObjID AS 'ACTUAL_PART_ID'
	goto fin
	end
print ' The qty is multiple so we need to do something else here'

if exists (SELECT ID FROM A_V_PROCEDURE_MONITORS WHERE PROCEDURE_ID = @procHistID)
	begin
	print 'This procedure has monitors'
	SELECT 'MULTIPLE_PICK_INDIVIDUAL' AS INITIAL_SCREEN_TYPE
	end
else
	begin
	print 'This procedure has NO monitors'
	SELECT 'MULTIPLE' AS INITIAL_SCREEN_TYPE
	end
	




fin:
