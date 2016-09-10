













CREATE               procedure dbo.A_SP_ORDER_ITEM_SET_PRICE 
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Get the parent ID and Parent Qty of ID = ' + @ID
declare @parentQTY as int, @parentID as varchar(50), @myPPL as varchar(50), 
		@myExID as varchar(50),	@specDisc as real, @qty float, @wt float,
		@wtType varchar(50),@sysID varchar(50)


SELECT @parentID = p.ID, @parentQTY = isNULL(p.QTY,1),
	@myPPL = c.PROD_PRICE_LIST,@sysID = c.PROC_SYS_ID,
	@specDisc = isNull(c.SPECIAL_DISCOUNT,0),
	@myExID = c.ADD_COST_ID, @qty = isNull(p.QTY,1),
	@wt = p.EST_WEIGHT, @wtType = p.EST_WEIGHT_UNIT
	FROM  A_ORDER_ITEMS c LEFT OUTER JOIN
          A_ORDER_ITEMS p ON c.PARENT = p.ID
	WHERE c.ID = @ID

print 'Special Discount = ' + isNull(convert(varchar(50),@specDisc),'NULL')
print 'PPL = ' + isNull(convert(varchar(50),@myPPL),'NULL')
declare @tester as varchar(50)
SELECT @tester = ID FROM A_ORDER_ITEMS WHERE PARENT = @ID
declare @myPrice REAL,@nonExChildPrice money,@exChildPrice money,@totalPrice money, @unitPrice money, @flatPrice money

if @sysID = 'SYS_SHIPPING' or @myExID is not null
	UPDATE A_ORDER_ITEMS SET EST_WEIGHT = @wt,EST_WEIGHT_UNIT = @wtType,QTY = @qty WHERE ID = @ID

if @tester is not null
	begin
	SELECT @nonExChildPrice = SUM((TOTAL_PRICE)) FROM A_ORDER_ITEMS WHERE PARENT = @ID AND ADD_COST_ID is null
	SELECT @exChildPrice = SUM((TOTAL_PRICE)) FROM A_ORDER_ITEMS WHERE PARENT = @ID AND ADD_COST_ID is not null
	set @myPrice = 0
	set @totalPrice = (isNull((1-(@specDisc/100)) * @exChildPrice,0)) + (isNull(@nonExChildPrice,0))
	end
else
	begin
	print 'Getting Price from Additional cost '
	exec A_SP_PROD_PRICE_LIST_EXTRA_COST_GET_PRICE_FOR_QTY 
			@myPrice OUTPUT, @flatPrice OUTPUT, 
			@unitPrice OUTPUT, @myExID, @qty, @wt, @wtType
	print 'MyUnit Price is ' + convert(nvarchar(50),@myPrice)
	set @totalPrice = (1-(@specDisc/100)) * @myPrice
	end		
print 'My Price = ' + isNull(convert(varchar(50),@myPrice),'NULL')
print 'My Total Price = ' + isNull(convert(varchar(50),@totalPrice),'NULL')
print 'My Special Disc = ' + isNull(convert(varchar(50),@specDisc),'NULL')

if @myExID is null
	begin
	print 'This is just an order item not a real money tracker so we need to do the qty the right way'
	UPDATE A_ORDER_ITEMS SET
		UNIT_PRICE = @unitPrice,
		FLAT_RATE = @flatPrice,
		PARENT_QTY = @parentQTY, 
		TOTAL_QTY = QTY,   --isNULL(@parentQTY,1) * 
		TOTAL_PRICE = @totalPrice
	WHERE ID = @ID
	end
else
	begin
	print 'This is a real money tracker so we need to make the qty = the parents and figure price on qty'
	UPDATE A_ORDER_ITEMS SET
		UNIT_PRICE = @unitPrice,
		FLAT_RATE = @flatPrice,
		PARENT_QTY = @parentQTY, 
		TOTAL_QTY = @parentQTY,
		TOTAL_PRICE = @totalPrice
	WHERE ID = @ID
	end

	

	

declare @pRunner varchar(50),@oldP varchar(50)
SELECT @pRunner = PARENT FROM A_ORDER_ITEMS WHERE ID = @ID
while @pRunner is not null
	begin
	set @oldP = @pRunner
	set @pRunner = null
	exec A_SP_ORDER_ITEM_SET_PRICE @oldP,@strNTLogin
	SELECT @pRunner = PARENT FROM A_ORDER_ITEMS WHERE ID = @oldP
	end
























