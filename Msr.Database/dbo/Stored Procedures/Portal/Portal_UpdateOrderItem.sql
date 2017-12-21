create procedure Portal_UpdateOrderItem
@DUE_DATE datetime,
@QTY float,
@CUST_LINE_ITEM varchar(50),
@ID varchar(50)
AS 

UPDATE A_ORDER_ITEMS SET DUE_DATE = @DUE_DATE, QTY = @QTY, CUST_LINE_ITEM =@CUST_LINE_ITEM  WHERE ID = @ID
