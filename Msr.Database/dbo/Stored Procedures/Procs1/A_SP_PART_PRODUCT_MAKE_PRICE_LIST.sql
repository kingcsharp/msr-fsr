CREATE PROCEDURE dbo.A_SP_PART_PRODUCT_MAKE_PRICE_LIST 
@partObjID varchar(50),
@prodObjID varchar(50),
@creatingPerson varchar(50)
AS
declare @myPrice money,@partID varchar(50)
SELECT @myPrice = PRICE,@partID = ID FROM A_PARTS_HISTORY WHERE OBJECT_ID = @partObjID
if @myPrice is null goto fin

declare @sql varchar(8000),@specList varchar(8000),@custList varchar(8000),
@custExList varchar(8000)

set @sql = 'SELECT CUST_ID FROM A_PARTS_FUTURE_CUSTOMERS WHERE PART_ID = ''' + @partID + ''''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@custList OUTPUT
set @sql = 'SELECT CUST_ID FROM A_PARTS_FUTURE_EXCEPTIONS WHERE PART_ID = ''' + @partID + ''''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@custExList OUTPUT
set @sql = 'SELECT SPEC_ID FROM A_PARTS_FUTURE_SPECIAL_DISTRIBUTIONS WHERE PART_ID = ''' + @partID + ''''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@specList OUTPUT

declare @pplObjID varchar(50), @messages varchar(2000)
exec A_SP_PROD_PRICE_LIST_UPDATE
	@pplObjID  OUTPUT,
	@messages  OUTPUT,
	null, --objID
	@prodObjID,
	@custList,--@CUSTOMERS varchar(8000),
	'UNIT', --@UNIT varchar(50),
	null,--@MIN_QUANTITY varchar(50),
	null, --@UNIT_PRICE varchar(50),
	null,--@EST_UNIT_PRICE varchar(50),
	null,--@EST_LABOR_PRICE varchar(50),
	null,--@EST_PARTS_PROV_STAY varchar(50),
	null,--@EST_PARTS_PROV_TAKE_BACK varchar(50),
	null,--@EST_PARTS_CONSUMED varchar(50),
	'ESTIMATED',--@INVOICE_FROM varchar(50),
	null,--@PRODUCTION_TIME varchar(50),
	'TIME_SYS_SECONDS',--@PRODUCTION_TIME_UNIT varchar(50),
	null,--@CAPACITY varchar(50),
	'minute',--@CAPACITY_UNIT varchar(50),
	@specList,-- varchar(8000),
	@custExList,--@CUSTOMER_EXCEPTIONS varchar(8000),
	'1',--@FOR_INDIVIDUAL_SALE tinyInt,
	@creatingPerson-- varchar(50)


declare @strPrice varchar(50)
set @strPrice = convert(varchar(50),@myPrice)
exec A_SP_PROD_PRICE_LIST_QUICK_ADD_UNIT_COST 
@pplObjID,
@strPrice,
'Cost of Item',
'QTY_BASED',
'UNIT',
@creatingPerson


UPDATE A_OBJECTS 
	SET STATUS = 'APPROVED',LOCKED_BY=NULL,LOCKED_BY_NAME=NULL 
	WHERE ID = @pplObjID

exec A_SP_PROD_PRICE_LIST_FINISH_APPROVAL_WF null,@pplObjID,@creatingPerson










fin:

