

CREATE   PROCEDURE dbo.A_SP_PROD_PRICE_LIST_FILL_CHILD_WITH_PRODUCT
@myProdPriceList varchar(50) OUTPUT,
@supplierID varchar(50),
@approvedObjID varchar(50),
@relationshipID varchar(50),
@PPL_ID varchar(50)
AS
declare @sysProcID as varchar(50),@custID as varchar(50)
SELECT TOP 1 @custID = CUST_ID FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS WHERE PP_LIST_ID = @PPL_ID
print 'Getting all the products that are for the part'
if @relationshipID = 'PARTS_PROVIDE_TAKE_BACK'
	begin
		set @sysProcID = 'SYS_PROVIDE_TAKE_BACK'
	end
if @relationshipID = 'LABOR_PROVIDE_TAKE_BACK'
	begin
		set @sysProcID = 'SYS_PROVIDE_TAKE_BACK'
	end
if @relationshipID = 'PARTS_PROVIDE_STAY'
	begin
		set @sysProcID = 'SYS_PROVIDE_AND_STAY'
	end
if @relationshipID = 'PARTS_PROVIDE_CONSUMED'
	begin
		set @sysProcID = 'SYS_PROVIDE_AND_CONSUMED'
	end
print 'I am going to look up products which have the part ' + @approvedObjID
print 'They also need the procedure = ' + @sysProcID

print 'SELECT TOP 1 * FROM A_V_PRODUCTS_WITH_PRICE_AND_PROC_DATA WHERE PART_ID = ''' + isNull(@approvedObjID,'NULL') + '''
AND SYSTEM_ID = ''' + isNULL(@sysProcID,'NULL') + ''' AND CUST_ID = ''' + @custID + ''''

SELECT Top 1 @myProdPriceList = PP_LIST_ID FROM A_V_PRODUCTS_WITH_PRICE_AND_PROC_DATA WHERE PART_ID = @approvedObjID
AND SYSTEM_ID = @sysProcID AND CUST_ID = @custID AND SUPPLIER_ID = @supplierID




