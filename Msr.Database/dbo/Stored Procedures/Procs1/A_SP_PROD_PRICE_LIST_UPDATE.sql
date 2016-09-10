



















CREATE                    PROCEDURE A_SP_PROD_PRICE_LIST_UPDATE
@newObjID varchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@objID varchar(50),
@PRODUCT varchar(50),
@CUSTOMERS varchar(8000),
@UNIT varchar(50),
@MIN_QUANTITY varchar(50),
@UNIT_PRICE varchar(50),
@EST_UNIT_PRICE varchar(50),
@EST_LABOR_PRICE varchar(50),
@EST_PARTS_PROV_STAY varchar(50),
@EST_PARTS_PROV_TAKE_BACK varchar(50),
@EST_PARTS_CONSUMED varchar(50),
@INVOICE_FROM varchar(50),
@PRODUCTION_TIME varchar(50),
@PRODUCTION_TIME_UNIT varchar(50),
@CAPACITY varchar(50),
@CAPACITY_UNIT varchar(50),
@SPECIAL_CUSTOMER varchar(8000),
@CUSTOMER_EXCEPTIONS varchar(8000),
@FOR_INDIVIDUAL_SALE tinyInt,
@strNTLogin varchar(50)
AS
declare @newID as nvarchar(50), @boolNew as smallInt
set @boolNew = 0
if @objID is null
begin
	set @boolNew = 1
	exec sp_getUniqueID3 @newID OUTPUT
	INSERT INTO A_PROD_PRICE_LIST_HISTORY ([ID],PRODUCT,MODBY,DRCM) VALUES
	(@newID, @PRODUCT, @strNTLogin, getDate())
	SELECT @objID = OBJECT_ID FROM A_PROD_PRICE_LIST_HISTORY WHERE ID = @newID
end
else
begin
	select @newID = ID FROM A_PROD_PRICE_LIST_HISTORY WHERE OBJECT_ID = @obJID
end

UPDATE A_PROD_PRICE_LIST_HISTORY SET
PRODUCT=@PRODUCT,
UNIT=@UNIT ,
MIN_QUANTITY=@MIN_QUANTITY ,
UNIT_PRICE=convert(money,@UNIT_PRICE) ,
EST_UNIT_PRICE=convert(money,@EST_UNIT_PRICE) ,
EST_LABOR_PRICE=convert(money,@EST_LABOR_PRICE) ,
EST_PARTS_PROV_STAY=convert(money,@EST_PARTS_PROV_STAY) ,
EST_PARTS_PROV_TAKE_BACK=convert(money,@EST_PARTS_PROV_TAKE_BACK) ,
EST_PARTS_CONSUMED=convert(money,@EST_PARTS_CONSUMED) ,
INVOICE_FROM=@INVOICE_FROM ,
PRODUCTION_TIME=@PRODUCTION_TIME ,
PRODUCTION_TIME_UNIT=@PRODUCTION_TIME_UNIT ,
CAPACITY=@CAPACITY ,
CAPACITY_UNIT=@CAPACITY_UNIT ,
FOR_INDIVIDUAL_SALE = @FOR_INDIVIDUAL_SALE,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE OBJECT_ID = @objID


CREATE TABLE #TempItems	(IT varchar(50))
Declare @it nvarchar(50)
Declare @curs Cursor

DELETE FROM A_PROD_PRICE_LIST_SPECIAL_DISTRIBUTIONS WHERE PP_LIST_ID = @newID
if @SPECIAL_CUSTOMER is not null
	begin
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @SPECIAL_CUSTOMER,','
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	INSERT INTO A_PROD_PRICE_LIST_SPECIAL_DISTRIBUTIONS (ID,PP_LIST_ID,SPEC_ID,DRCM,MODBY)
		VALUES(newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
	end

DELETE FROM A_PROD_PRICE_CUSTOMERS WHERE PP_LIST_ID = @newID
if @CUSTOMERS is not null
	begin
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @CUSTOMERS,','
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	INSERT INTO A_PROD_PRICE_CUSTOMERS (ID,PP_LIST_ID,CUST_ID,DRCM,MODBY)
		VALUES(newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
	end

DELETE FROM A_PROD_PRICE_LIST_EXCEPTIONS WHERE PP_LIST_ID = @newID
if @CUSTOMER_EXCEPTIONS is not null
	begin
DELETE FROM  #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @CUSTOMER_EXCEPTIONS,','
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding customer exception = ' + @it
	INSERT INTO A_PROD_PRICE_LIST_EXCEPTIONS (ID,PP_LIST_ID,CUST_ID,DRCM,MODBY)
		VALUES(newID(),@newID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
	end

exec A_SP_PROD_PRICE_SET_REAL_CUSTOMERS @newID,@strNTLogin

if @PRODUCT is null goto fin
print 'exec A_SP_PROD_PRICE_LIST_ADD_CHILDREN_AND_FILL_THEM ''' + @PRODUCT + ''','''+ @newID + ''',''' + @strNTLogin + ''''
exec A_SP_PROD_PRICE_LIST_ADD_CHILDREN_AND_FILL_THEM @PRODUCT,@newID,@strNTLogin
exec A_SP_PROD_PRICE_LIST_SET_MY_PRICE @newID
fin:
set @newObjID = @objID
print 'Finished Updating the product price list'










