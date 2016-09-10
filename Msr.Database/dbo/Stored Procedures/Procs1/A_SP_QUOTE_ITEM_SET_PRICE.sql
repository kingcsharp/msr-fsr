








CREATE          procedure dbo.A_SP_QUOTE_ITEM_SET_PRICE 
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Get the parent ID and Parent Qty of ID = ' + @ID
declare @parentQTY as int
declare @parentID as varchar(50)
declare @myPPL as varchar(50)
declare @specDisc as real
SELECT @parentID = p.ID, @parentQTY = isNULL(p.QTY,1),
	@myPPL = c.PROD_PRICE_LIST,
	@specDisc = isNull(c.SPECIAL_DISCOUNT,0)
	FROM  A_QUOTE_ITEMS c LEFT OUTER JOIN
          A_QUOTE_ITEMS p ON c.PARENT = p.ID
	WHERE c.ID = @ID

print 'SELECT  p.ID, p.QTY,c.PROD_PRICE_LIST,
	c.SPECIAL_DISCOUNT FROM A_QUOTE_ITEMS p, A_QUOTE_ITEMS c
	WHERE c.PARENT =+ p.ID AND c.ID = ''' + @ID + ''''

print 'Special Discount = ' 
print @specDisc
print 'PPL = '
print @myPPL
print 'Get a total of the children unit prices'
declare @tester as varchar(50)
SELECT @tester = ID FROM A_QUOTE_ITEMS WHERE PARENT =@ID
declare @myPrice REAL
declare @childPrice REAL
declare @totalPrice REAL



if @tester is not null
	begin
	SELECT @childPrice = SUM((TOTAL_PRICE)) FROM A_QUOTE_ITEMS WHERE PARENT = @ID
	print 'The price of my children is '
	print @childPrice
	SELECT @myPrice = UNIT_PRICE FROM A_QUOTE_ITEMS WHERE ID = @ID
	if @myPrice is NULL
		begin
		print 'Getting Price from PPL '
		print 'SELECT UNIT_PRICE FROM A_V_PROD_PRICE_LIST_BY_APPROVED_ID WHERE ID = ' + isNULL('''' + @myPPL + '''',NULL)
		SELECT @myPrice = UNIT_PRICE FROM A_V_PROD_PRICE_LIST_BY_APPROVED_ID WHERE ID = @myPPL
		end
	print 'My Unit Price = ' + convert(varchar(50),@myPrice )
	set @totalPrice = isNull((1-(@specDisc/100)) * @myPrice,0) + isNull(@childPrice,0)
	end
else
	begin
	SELECT @myPrice = UNIT_PRICE FROM A_QUOTE_ITEMS WHERE ID = @ID
	if @myPrice is NULL
		begin
		print 'Getting Price from PPL '
		print 'SELECT UNIT_PRICE FROM A_V_PROD_PRICE_LIST_BY_APPROVED_ID WHERE ID = ' + isNULL('''' + @myPPL + '''',NULL)
		SELECT @myPrice = UNIT_PRICE FROM A_V_PROD_PRICE_LIST_BY_APPROVED_ID WHERE ID = @myPPL
		end 
	print 'MyUnit Price is '
	print convert(nvarchar(50),@myPrice)
	set @totalPrice = (1-(@specDisc/100)) * @myPrice
	end		

UPDATE A_QUOTE_ITEMS SET
	UNIT_PRICE = @myPrice,
	PARENT_QTY = @parentQTY, 
	TOTAL_QTY = isNULL(@parentQTY,1) * QTY,
	TOTAL_PRICE = (isNULL(@parentQTY,1) * QTY) * @totalPrice
WHERE ID = @ID

SELECT * FROM A_QUOTE_ITEMS WHERE ID = @ID
	
	



















