
CREATE PROCEDURE DBO.A_SP_ORDER_ASSIGN_PRICE
	@objID nvarchar(50)
as
begin transaction
print 'Assigning the price to Order'
declare @ordHistID varchar(50),@quoteHistID varchar(50),@myPrice float
SELECT @ordHistID = ID FROM A_ORDERS_HISTORY WHERE OBJECT_ID = @objID
SELECT @quoteHistID = ID FROM A_QUOTES_HISTORY WHERE ORDER_ID = @objID
if @quoteHistID is not null
	begin
	print 'quotehistID = ' + @quoteHistID
	SELECT @myPrice = sum(TOTAL_PRICE) FROM A_ORDER_ITEMS WHERE 
				QUOTE_ID = @quoteHistID
			 	AND PARENT is not null 
				AND PURCHASE_HIST_ID IS NULL
	print 'got a quote and made the price '
	print @myPrice
	end
else
	SELECT @myPrice = sum(TOTAL_PRICE) FROM A_ORDER_ITEMS WHERE 
				ORDER_ID = @ordHistID
			 	AND PARENT is not null 
				AND PURCHASE_HIST_ID IS NULL
	
UPDATE A_ORDERS_HISTORY SET PRICE = @myPRice WHERE OBJECT_ID = @objID


if @@ERROR <> 0 GOTO problem

fin:
if @@trancount > 0 	commit transaction
return 0
problem:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ORDERS_FINISH_WF and we will terminate and not finish anything '
return 1














