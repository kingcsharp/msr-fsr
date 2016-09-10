



CREATE     PROCEDURE dbo.A_SP_PROD_PRICE_LIST_SET_MY_PRICE
@ppID varchar(50)
AS
print 'Setting the total price for ' + @ppID
declare @total as money
print 'SELECT PRICE FROM A_V_PROD_PRICE_LIST_CHILDREN_PROD_FILLS WHERE PARENT = ''' + @ppID + ''''
SELECT @total = isNull(SUM(PRICE),0) FROM A_V_PROD_PRICE_LIST_CHILDREN_PROD_FILLS WHERE PARENT = @ppID
print 'Total is ' + isNull(convert(varchar(50),@total),'NULL')
declare @ecTot as money
SELECT @ecTot = isNull(sum(UNIT_PRICE),0) FROM A_PROD_PRICE_LIST_EXTRA_COSTS WHERE PROD_PRICE_LIST = @ppID
print 'ECTotal is ' + isNull(convert(varchar(50),@ecTot),'NULL')
UPDATE A_PROD_PRICE_LIST_HISTORY SET UNIT_PRICE = (isNull(@ecTot,0) + isNull(@total,0))  WHERE ID = @ppID

print 'Setting the unit type for this product'
print 'Since the unit is probably based on the extra costs check to see if their are conflicting'
print 'extra costs which will make it impossible to determine what the true cost per whatever is'
declare @numUnits int
SELECT @numUnits = COUNT(DISTINCT UNIT)
	FROM A_PROD_PRICE_LIST_EXTRA_COSTS
	WHERE PROD_PRICE_LIST = @ppID

declare @myUnit varchar(50)
if @numUnits = 1 
	begin
	print 'Num Units = 1'
	SELECT TOP 1 @myUnit = UNIT FROM A_PROD_PRICE_LIST_EXTRA_COSTS	WHERE PROD_PRICE_LIST = @ppID
	end
else set @myUnit = 'UNIT'

UPDATE A_PROD_PRICE_LIST_HISTORY SET UNIT = @myUnit WHERE ID = @ppID





fin:


