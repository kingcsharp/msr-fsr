











CREATE            PROCEDURE dbo.A_SP_PROD_PRICE_LIST_ADD_CHILDREN_AND_FILL_THEM
@PRODUCT varchar(50),
@pplID varchar(50),
@strNTLogin varchar(50)

AS
DELETE FROM A_PROD_PRICE_LIST_SUB_PRICE_LISTS WHERE PARENT = @pplID
declare @curs as CURSOR
declare @sysProcID as varchar(50)
print 'We need to determine if this is a system procedure product.  If it is then we do not need to do any of this stuff'
print 'SELECT PROC_SYS_ID FROM A_V_PRODUCTS_APPROVED_DATA_BY_ID WHERE ID ='  + isnull('''' + @PRODUCT + '''','NULL')
SELECT @sysProcID = PROC_SYS_ID FROM A_V_PRODUCTS_APPROVED_DATA_BY_ID WHERE ID = @PRODUCT
if @sysProcID in ('SYS_E_ACCESS','SYS_PROVIDE_TAKE_BACK','SYS_PROVIDE_AND_STAY','SYS_PROVIDE_AND_CONSUME') goto fin

print 'This is not a system procedure so we need to create the children links if they do not exist'
declare @procID as varchar(50),@procObjID as varchar(50),@supID as varchar(50)
print 'SELECT PROC_HIST_ID,PROC_OBJ_ID FROM A_V_PRODUCTS_APPROVED_DATA_BY_ID WHERE ID =''' + @PRODUCT + ''''
SELECT @supID = SUPPLIER_ID,@procID = PROC_HIST_ID,@procObjID = PROC_OBJ_ID FROM A_V_PRODUCTS_APPROVED_DATA_BY_ID WHERE ID = @PRODUCT
if @procID is null goto fin
print 'Now I am going to figure out all the sub products for this product price list and try to get a price'
print 'The product we are working with has a procedure Obj ID of ' + @procObjID
print 'We need to insert the Approved Object IDs into a table here if they are not there already'

CREATE TABLE #CHILDREN (APPROVED_OBJ_ID varchar(50),RELATIONSHIP varchar(50),QTY REAL,UNIT_TYPE varchar(50))
INSERT INTO #CHILDREN 
	SELECT APPROVED_OBJECT_ID,RELATIONSHIP,SUM(QTY),UNIT_TYPE
		FROM A_V_PROCEDURE_OBJ_LINK_DATA_FOR_SUMMING 
		WHERE PROCEDURE_ID = @procID AND APPROVED_OBJECT_ID IS NOT NULL
		GROUP BY APPROVED_OBJECT_ID,RELATIONSHIP,UNIT_TYPE

print 'Got a temp table that looks like this'
SELECT 'TempChild', * FROM #CHILDREN
print 'The Temp table is good'

INSERT INTO A_PROD_PRICE_LIST_SUB_PRICE_LISTS 
	(ID,PARENT,DRCM,MODBY,APPROVED_OBJ_ID,RELATIONSHIP,QTY,UNIT_TYPE)
	SELECT newID() AS ID,@pplID AS PARENT,getDate() AS DATE,@strNTLogin AS MODBY,
		APPROVED_OBJ_ID,RELATIONSHIP,QTY AS QTY,UNIT_TYPE FROM #CHILDREN l WHERE
		RELATIONSHIP <> 'ROLE_TO_VIEW' AND
		NOT EXISTS (SELECT ID FROM A_PROD_PRICE_LIST_SUB_PRICE_LISTS s WHERE s.APPROVED_OBJ_ID = l.APPROVED_OBJ_ID AND PARENT = @pplID AND RELATIONSHIP = l.RELATIONSHIP)

print 'OK So now we have the sub products all in the sub products table'
print 'Now we need to put some products that we have in there'
print 'First we need to get the supplier of this product'
declare @supplier as varchar(50)
SELECT @supplier = SUPPLIER_ID FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @PRODUCT
print 'The suppplier ID is ' + @supplier

declare @subID as varchar(50), @APPROVED_OBJ_ID as varchar(50),
	@QTY as real, @QTY_TYPE as varchar(50), @RELATIONSHIP as varchar(50)

set @curs = Cursor For 
	SELECT s.ID,s.APPROVED_OBJ_ID,s.QTY,s.RELATIONSHIP 
	FROM A_PROD_PRICE_LIST_SUB_PRICE_LISTS s
	WHERE s.PARENT = @pplID
open @curs
Fetch Next from @curs Into @subID,@APPROVED_OBJ_ID,@QTY,@RELATIONSHIP
declare @myFillChild as varchar(50)
while (@@fetch_status = 0)
Begin
	set @myFillChild = null
	print 'Looking at fill object = ' + @APPROVED_OBJ_ID + ' with a relationship of ' + @RELATIONSHIP
	exec A_SP_PROD_PRICE_LIST_FILL_CHILD_WITH_PRODUCT	
		@myFillChild OUTPUT,@supplier,@APPROVED_OBJ_ID,@RELATIONSHIP,@pplID
	print 'The Fill would be using Prod Price List # ' + @myFillChild
	UPDATE A_PROD_PRICE_LIST_SUB_PRICE_LISTS SET CHILD = @myFillChild WHERE ID = @subID
	Fetch Next from @curs Into @subID,@APPROVED_OBJ_ID,@QTY,@RELATIONSHIP
End
close @curs
Deallocate @curs

print 'Now that we updated all those we need to set the price'
exec A_SP_PROD_PRICE_LIST_SET_MY_PRICE @pplID




fin:
select 'It Worked'
SELECT * FROM A_V_PROD_PRICE_LIST_CHILDREN_PROD_FILLS WHERE PARENT = @pplID











