

CREATE          PROCEDURE dbo.A_SP_PROD_PRICE_LIST_GET_PROD_INFO_FOR_PRICE
@approvedObjID varchar(50),
@relationshipID varchar(50),
@PPL_ID varchar(50),
@units real,
@PP_CHILD_LIST_ID varchar(50)
AS
declare @sysProcID as varchar(50),@custID as varchar(50),@supID as varchar(50)
create TABLE #tempCustomerPPLTable (ID varchar(50))
INSERT INTO #tempCustomerPPLTable SELECT CUST_ID FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS WHERE PP_LIST_ID = @PPL_ID

SELECT @supID = ph.SUPPLIER_ID
	FROM A_PRODUCTS p 
		INNER JOIN A_PRODUCTS_HISTORY ph ON p.HISTORY_REF_ID = ph.ID 
		INNER JOIN A_PROD_PRICE_LIST_HISTORY ppl ON p.ID = ppl.PRODUCT
		WHERE ppl.ID = @PPL_ID

print 'The supplier is ' + @supID

print 'Getting all the products that are for the part'
if @relationshipID = 'PARTS_PROVIDE_TAKE_BACK'
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
if @relationshipID = 'LABOR_PROVIDE_TAKE_BACK'
	begin
		set @sysProcID = 'SYS_PROVIDE_TAKE_BACK'
	end


print 'I am going to look up products which have the obj ID =  ' + @approvedObjID
print 'They also need the procedure = ' + @sysProcID

print 'declare @sysProcID as varchar(50),@custID as varchar(50),@supID as varchar(50)
create TABLE #tempCustomerPPLTable (ID varchar(50))
INSERT INTO #tempCustomerPPLTable SELECT CUST_ID FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS WHERE PP_LIST_ID = ''' + @PPL_ID + ''''
print 'SELECT ''' + isnull(@approvedObjID,'') + ''' as APPROVED_OBJ_ID,
	''' + @PP_CHILD_LIST_ID + ''' AS LINK_ID,
	ID,NAME,
SUPPLIER_ID,
PROCEDURE_ID,
UNIT_PRICE,
COMMENTS,
SYSTEM_ID,
PROC_NAME,
PART_ID,
PP_LIST_ID,' + isnull(convert(nvarchar(50),@UNITS),'') + ' AS UNITS_NEEDED,' + isnull(convert(nvarchar(50),@UNITS),'1') + ' * UNIT_PRICE AS TOTAL_PRICE 
	FROM A_V_PRODUCTS_WITH_PRICE_AND_PROC_DATA WHERE PART_ID = ''' + isnull(@approvedObjID,'n') + '''
	AND SYSTEM_ID = ''' + isnull(@sysProcID,'n') + ''' AND CUST_ID IN (SELECT ID FROM #tempCustomerPPLTable) AND SUPPLIER_ID = ''' + isnull(@supID,'n') + ''''

SELECT DISTINCT @approvedObjID as APPROVED_OBJ_ID,@PP_CHILD_LIST_ID AS LINK_ID,
	(@UNITS / STD_UNIT_QTY) AS UNITS_NEEDED,(@UNITS / STD_UNIT_QTY) * UNIT_PRICE AS TOTAL_PRICE,ID,NAME,SUPPLIER_ID,
	PROCEDURE_ID,UNIT_PRICE,COMMENTS,SYSTEM_ID,PROC_NAME,PART_ID,PP_LIST_ID,UNIT
	FROM A_V_PRODUCTS_WITH_PRICE_AND_PROC_DATA WHERE PART_ID = @approvedObjID
	AND SYSTEM_ID = @sysProcID AND CUST_ID IN (SELECT ID FROM #tempCustomerPPLTable) AND SUPPLIER_ID = @supID














