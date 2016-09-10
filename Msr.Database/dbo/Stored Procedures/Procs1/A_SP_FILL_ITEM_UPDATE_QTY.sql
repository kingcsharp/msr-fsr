CREATE PROCEDURE dbo.A_SP_FILL_ITEM_UPDATE_QTY
@fillItemID varchar(50),
@qty float,
@strNTLogin varchar(50)
AS
print 'Updating the Qty to ' + convert(varchar(50),@qty) + ' for fill item = ' + @fillItemID
declare @oldQty float,@oldPrice money,@itemPrice money,@objID varchar(50)
SELECT @oldQty = FILL_QTY,@oldPrice = PRICE,@objID = FILL_OBJ_ID
	FROM A_FILLS WHERE ID = @fillItemID
set @itemPrice = @oldPrice / @oldQty
print 'Item Price = ' + convert(varchar(50),@itemPrice)
UPDATE A_FILLS SET FILL_QTY = @qty,PRICE = @qty * @itemPrice WHERE ID = @fillItemID
UPDATE A_ACTUAL_PARTS_HISTORY SET QTY = @qty WHERE ID IN
	(SELECT HISTORY_REF_ID FROM A_ACTUAL_PARTS WHERE ID = @objID)
	

SELECT 'SUCCESSFULLY UPDATED QTY' AS RESULT
