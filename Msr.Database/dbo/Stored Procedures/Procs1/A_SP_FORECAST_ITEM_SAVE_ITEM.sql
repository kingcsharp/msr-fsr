



CREATE               PROCEDURE A_SP_FORECAST_ITEM_SAVE_ITEM
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID varchar(50),
@F_OBJ_ID varchar(50),
@ACCOUNT varchar(50),
@EXPENSE_TYPE varchar(50),
@QTY float,
@IN_AMT varchar(50),
@PERCENT varchar(50),
@CONFIDENCE varchar(50),
@NOTE nvarchar(2000),
@EST_QUAL_START_DATE datetime,
@ACT_QUAL_START_DATE datetime,
@EST_FIRST_PURCHASE_DATE datetime,
@ACT_FIRST_PURCHASE_DATE datetime,
@STATUS nvarchar(1000),
@PRIORITY varchar(50),
@SUPPLIER_OWNER nvarchar(200),
@CUSTOMER_OWNER nvarchar(200),
@UNIT_PRICE float,
@strNTLogin varchar(50)
AS
set @EST_QUAL_START_DATE = dbo.timeToGrenich(@EST_QUAL_START_DATE,@strNTLogin)
set @ACT_QUAL_START_DATE = dbo.timeToGrenich(@ACT_QUAL_START_DATE,@strNTLogin)
set @EST_FIRST_PURCHASE_DATE = dbo.timeToGrenich(@EST_FIRST_PURCHASE_DATE,@strNTLogin)
set @ACT_FIRST_PURCHASE_DATE = dbo.timeToGrenich(@ACT_FIRST_PURCHASE_DATE,@strNTLogin)
set @qty = isNull(@qty,0)
declare @FID as varchar(50),@AMT as money
if @EXPENSE_TYPE is null
	begin
	set @EXPENSE_TYPE = 'REVENUE'
	end
set @AMT = CONVERT(money,@IN_AMT)
if @ID is null
	begin
		print 'ID is Null we need to create this Forecast Item'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		SELECT @FID = ID FROM A_FORECASTS_HISTORY WHERE OBJECT_ID = @F_OBJ_ID
		INSERT INTO A_FORECAST_ITEMS(ID,DATE_ADDED,FORECAST_ID,ACCOUNT_ID,MODBY,DRCM) VALUES(@newID,getDate(),@FID,@ACCOUNT,@strNTLogin,getDATE())
	end
SELECT @FID = FORECAST_ID FROM A_FORECAST_ITEMS WHERE ID = @ID
print'Updating the Forecast Item Number = ' + @ID
declare @dateAdded datetime
SELECT @dateAdded = DATE_ADDED FROM A_FORECAST_ITEMS WHERE ID = @ID
if @dateAdded is null
	UPDATE A_FORECAST_ITEMS SET	DATE_ADDED = getDate() WHERE ID = @ID



UPDATE A_FORECAST_ITEMS SET
ACCOUNT_ID = @ACCOUNT,
F_TYPE = @EXPENSE_TYPE,
QTY = @QTY,
F_AMT = @AMT,
PERCENT_OF_REV = @PERCENT,
CONFIDENCE = @CONFIDENCE,
NOTE = @NOTE,
EST_QUAL_START_DATE = @EST_QUAL_START_DATE,
ACT_QUAL_START_DATE =@ACT_QUAL_START_DATE,
EST_FIRST_PURCHASE_DATE =@EST_FIRST_PURCHASE_DATE ,
ACT_FIRST_PURCHASE_DATE =@ACT_FIRST_PURCHASE_DATE ,
STATUS =@STATUS ,
PRIORITY =@PRIORITY ,
SUPPLIER_OWNER =@SUPPLIER_OWNER ,
CUSTOMER_OWNER =@CUSTOMER_OWNER,
UNIT_PRICE = @UNIT_PRICE
WHERE ID = @ID

print 'Inserted'
print 'really inserted'
if @UNIT_PRICE IS NULL AND @qty is not null and @qty <> 0.0
	begin
	print 'in here'
	declare @figU money
	exec A_SP_FORECAST_ITEM_FIGURE_AVG_PRICE @figU OUTPUT,@ID,@strNTLogin
	UPDATE A_FORECAST_ITEMS SET UNIT_PRICE = convert(money,@figU), F_AMT=(convert(money,@figU) * convert(money,@QTY)) WHERE ID = @ID
	print 'out of here'
	end
print 'h'
if @UNIT_PRICE IS not NULL AND @qty is not null
	begin
	print 'ina here'
	UPDATE A_FORECAST_ITEMS SET UNIT_PRICE = convert(money,@UNIT_PRICE), F_AMT=(convert(money,@UNIT_PRICE) * convert(money,@QTY)) WHERE ID = @ID
	print 'out here'
	end

print '1'
set @newID = @ID



exec A_SP_FORECAST_ITEM_FIX_DISTRIBUTION @ID,@strNTLogin
exec A_FORECAST_ITEM_FIGURE_INVOICED_AMOUNTS @ID,@strNTLogin
exec A_SP_FORECAST_ITEM_FIGURE_PROGRESS @ID,@strNTLogin

















