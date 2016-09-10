

CREATE  PROCEDURE dbo.A_SP_PRODUCT_GET_PURCHASING_DETAIL_DATA 
@prodID varchar(50),
@pplID varchar(50),
@strNTLogin varchar(50)
AS
declare @myRootCo varchar(50),@myCo varchar(50)
SELECT @myRootCo = ROOT_COMPANY,@myCo = COMPANY FROM A_V_PEOPLE_APPROVED_DATA

print 'Getting the product purchasing details info.'
CREATE TABLE #tempP (
	PPL_ID varchar(50),
	PRODUCT_NAME nvarchar(1000),
	PRODUCT_ID varchar(50),
	MIN_QUANTITY float,
	PRODUCTION_TIME float,
	PRODUCTION_TIME_UNIT varchar(50),
	CAPACITY float,
	CAPACITY_UNIT varchar(50),
	SUPPLIER_ID varchar(50),
	SUPPLIER_PN nvarchar(100),
	SUPPLIER_PART_NAME nvarchar(200),
	CUSTOMER_PN nvarchar(50),
	CUSTOMER_PART_NAME nvarchar(200),
	SUPPLIER_NAME varchar(50),
	PROCEDURE_ID varchar(50),
	PROC_SYS_ID varchar(50),
	SUP_PART_ID varchar(50),
	CUST_PART_ID varchar(50),
	SUP_OBJ_ID varchar(50),
	CUST_OBJ_ID varchar(50),
	AVAILABILITY tinyInt
)
INSERT INTO #tempP (
	PPL_ID,
	PRODUCT_NAME,
	PRODUCT_ID,
	MIN_QUANTITY,
	PRODUCTION_TIME,
	PRODUCTION_TIME_UNIT,
	CAPACITY,
	CAPACITY_UNIT
)
	SELECT 
	ID,
	PRODUCT_NAME,
	PRODUCT,
	MIN_QUANTITY,
	PRODUCTION_TIME,
	PRODUCTION_TIME_UNIT,
	CAPACITY,
	CAPACITY_UNIT
		FROM A_V_PROD_PRICE_LIST_APPROVED_DATA WHERE ID = @pplID

declare @supplierID varchar(50),@procID varchar(50),@appObjID varchar(50)
SELECT @supplierID = SUPPLIER_ID,@procID = PROCEDURE_ID,@appObjID = APP_OBJECT 
	FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = (SELECT PRODUCT_ID FROM #tempP)
UPDATE #tempP SET SUPPLIER_ID = @supplierID,PROCEDURE_ID = @procID
UPDATE #tempP SET SUPPLIER_NAME = (SELECT NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplierID)
UPDATE #tempP SET AVAILABILITY = (SELECT AVAILABILITY FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = (SELECT PRODUCT_ID FROM #tempP)) 

declare @histID varchar(50),@objTable varchar(50)
SELECT @histID = OBJ_ID, @objTable = OBJ_TABLE 
	FROM A_OBJECTS 
	WHERE ROOT = @appObjID AND STATUS LIKE 'APPROVED%'


declare @supPartHistID varchar(50),@supPartNum nvarchar(1000),@custPartID varchar(50), @custPartNum nvarchar(1000),
	@supPartName nvarchar(1000),@custPartName nvarchar(1000),@custPartHistID varchar(50), @supObjID varchar(50),
	@custObjID varchar(50),@supPartID varchar(50)
if @objTable = 'A_PARTS_HISTORY' 
	begin
	SELECT @supPartHistID = ID,@supPartNum = COMPANY_PART_NUMBER,@supPartName = NAME,@supObjId = OBJECT_ID
		FROM A_PARTS_HISTORY WHERE ID = @histID
	SELECT @supPartID = ROOT FROM A_OBJECTS WHERE ID = @supObjID
	UPDATE #tempP SET SUP_OBJ_ID= @supObjID,SUPPLIER_PN = @supPartNum,SUPPLIER_PART_NAME = @supPartName,SUP_PART_ID = @appObjID

	end



declare @procSysID varchar(50)
SELECT @procSysID =  SYSTEM_ID
	FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = (SELECT PROCEDURE_ID FROM #tempP)
UPDATE #tempP SET PROC_SYS_ID = @procSysID
--if @procSysID = 'SYS_PROVIDE_AND_STAY'
--	begin
	print 'This is a provide and stay'
	if @supplierID <> @myCo and @supplierID <> @myRootCo
		begin
		print 'We need to figure out the customer Part Number too.'
		print 'The supplier part history number is ' + @supPartID
		SELECT @custPartHistID = dbo.A_SP_PART_FIND_MY_RELATED_PART(@supPartID,@strNTLogin)
		print 'The Part ID = ' + isNull(@custPartHistID,'NULL')
		if @custPartHistID is not null
			begin
			print 'Looking for my part' + @custPartHistID
			SELECT @custPartNum = COMPANY_PART_NUMBER,@custPartName = NAME,@custPartID = OBJECT_ID,@custObjId = OBJECT_ID
				FROM A_PARTS_HISTORY WHERE ID = @custPartHistID
			UPDATE #tempP SET CUST_OBJ_ID= @custObjID,CUSTOMER_PN = @custPartNum,CUSTOMER_PART_NAME = @custPartName,CUST_PART_ID = @custPartID
			end


		end

--	end








SELECT * FROM #tempP
DROP TABLE #tempP




