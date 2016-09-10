


CREATE     PROCEDURE dbo.A_SP_PRODUCT_GET_OPPURTUNITY_NUMBERS
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
print 'Create the Temp Table'
CREATE TABLE #TempItems	(ID varchar(50),TOT int,HAVE int,NEED int,CUST_ID varchar(50))
Declare @it nvarchar(50)
declare @tot int,@have int,@need int
Declare @curs Cursor
print 'We are making a cursor'


set @curs = Cursor For SELECT DISTINCT(CUR_OWNER) FROM A_V_PRODUCTS_WITH_ACTUAL_PARTS_THEY_ARE_USED_ON 
	WHERE PRODUCT_ID = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Finding oppurtunity data for customer = ' + @it
	SELECT @TOT =  SUM(QTY) FROM A_V_PRODUCTS_WITH_ACTUAL_PARTS_THEY_ARE_USED_ON
		WHERE PRODUCT_ID = @ID AND CUR_OWNER = @it
	print 'Total possible = '
	print @TOT
	print 'The Product ID = ' + @ID + ' And the cur oxer = ' + @it
	SELECT @have = SUM(QTY) FROM A_V_PRODUCTS_WITH_ACTUAL_PARTS_THEY_ARE_USED_ON
		WHERE PRODUCT_ID = @ID AND CUR_OWNER = @it AND PROD_STATUS = 'PROD_INSTALLED'
	print 'Total Have = '
	set @have = isNull(@have,0)
	print @have
	set @need = @tot - @have
	INSERT INTO #TempItems (ID,TOT,HAVE,NEED,CUST_ID) VALUES 
	(@ID,@tot,@have,@need,@it)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


SELECT t.*
,c.NAME AS CO_NAME
,p.NAME AS PRODUCT_NAME
,p.ID AS PRODUCT_ID
FROM #TempItems t
,A_V_COMPANIES_APPROVED_DATA c
,A_V_PRODUCTS_APPROVED_DATA p
where 
t.CUST_ID = c.ID 
AND t.ID = p.ID










