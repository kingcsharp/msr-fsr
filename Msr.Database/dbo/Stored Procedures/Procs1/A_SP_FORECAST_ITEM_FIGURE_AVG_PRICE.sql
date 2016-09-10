




CREATE     PROCEDURE dbo.A_SP_FORECAST_ITEM_FIGURE_AVG_PRICE
@amt money OUTPUT,
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Figuring out the average price'
declare @prodID as varchar(50),@custID as varchar(50),@supplierID as varchar(50),
	@pplHistID varchar(50), @pplExCostID varchar(50),@qty float, @myPrice money

SELECT @prodID = PRODUCT_ID,@custID = CUSTOMER_ID, @supplierID = SUPPLIER_ID, @qty = isNull(QTY,1.0)
	FROM A_V_FORECAST_ITEM_EDIT_DATA WHERe ID = @ID

if @qty = 0.0 set @qty = 1.0
print 'The prod is ' + @prodID
print 'The cust is ' + @custID
print 'The supplier is ' + @supplierID
print 'The FI is ' + @ID

print 'Figuring out the average Price'
Declare @it nvarchar(50)
Declare @curs Cursor
CREATE TABLE #tempPrices (price money)

if @prodID is not null and @custID is not null and @supplierID is not null
	set @curs = Cursor For SELECT HISTORY_REF_ID FROM A_V_PROD_PRICE_LIST_APPROVED_DATA WHERE
		PRODUCT = @prodID AND SUPPLIER_ID = @supplierID AND 
		exists(SELECT CUST_ID FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS 
				WHERE PP_LIST_ID = HISTORY_REF_ID AND CUST_ID = @custID)

if @prodID is not null and @custID is null and @supplierID is not null
	set @curs = Cursor For SELECT HISTORY_REF_ID FROM A_V_PROD_PRICE_LIST_APPROVED_DATA WHERE
		PRODUCT = @prodID AND SUPPLIER_ID = @supplierID 


if @prodID is null and @custID is not null and @supplierID is not null
	set @curs = Cursor For SELECT HISTORY_REF_ID FROM A_V_PROD_PRICE_LIST_APPROVED_DATA WHERE
		SUPPLIER_ID = @supplierID AND 
		exists(SELECT CUST_ID FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS 
				WHERE PP_LIST_ID = HISTORY_REF_ID AND CUST_ID = @custID)


open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'looking at price list = ' + @it
	SELECT @pplExCostID = ID FROM A_PROD_PRICE_LIST_EXTRA_COSTS WHERE PROD_PRICE_LIST = @it
	exec A_SP_PROD_PRICE_LIST_EXTRA_COST_GET_PRICE_FOR_QTY 
			@myPrice OUTPUT, null, null, @pplExCostID, @qty, null, null
	insert into #tempPrices values(@myPrice / isNull(@qty,1))
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

SELECT @amt = AVG(price) from #tempPrices

SELECT AVG(price) AS AVG_PRICE from #tempPrices







