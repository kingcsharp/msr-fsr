






CREATE       PROCEDURE A_SP_ORDERS_UPDATE_ORDER
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@objID varchar(50),
@ID varchar(50),
@CUSTOMER_PERSON varchar(50),
@CUSTOMER_CO varchar(200),
@DESCRIPTION varchar(200),
@BUDGETARY_ONLY varchar(50),
@SUPPLIERS_TO_SHARE_WITH varchar(8000),
@EXPIRATION_DATE  varchar(50),
@strNTLogin varchar(50)
AS
print 'Updating an Order'
if @CUSTOMER_PERSON is null
	set @CUSTOMER_PERSON = @strNTLogin

if @objID is null
	begin
		print 'ID is Null we need to create this Order'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_ORDERS_HISTORY(ID,DESCRIPTION,MODBY,DRCM,PROGRESS) 
			VALUES(@newID,@DESCRIPTION,@strNTLogin,getDATE(),'CREATING')
		SELECT @newID = OBJECT_ID FROM A_ORDERS_HISTORY WHERE ID = @ID
	end
else
	begin
		print 'The ID is not null so we are just updating Account objectID='+@objID
		set @newID = @objID
	end

if @CUSTOMER_PERSON is not null and @CUSTOMER_CO is NULL
	begin
	SELECT @CUSTOMER_CO = COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @CUSTOMER_PERSON
	end

--Clear out all the purchses if the customer_co is not the same as the old one.
--if not(exists(SELECT CUSTOMER_CO FROM A_ORDERS_HISTORY WHERE OBJECT_ID = @newID and CUSTOMER_CO = @CUSTOMER_CO))
--	DELETE FROM A_ORDER_ITEMS WHERE ORDER_ID = @ID



UPDATE A_ORDERS_HISTORY SET
CUSTOMER_PERSON = @CUSTOMER_PERSON,
CUSTOMER_CO = @CUSTOMER_CO,
DESCRIPTION = @DESCRIPTION,
BUDGETARY_ONLY = @BUDGETARY_ONLY,
EXPIRATION_DATE = @EXPIRATION_DATE,
MODBY = @strNTLogin,
DRCM = getDate()
WHERE OBJECT_ID = @newID



print 'Updating Supplier to share with list for the Order'
print 'First delete all the ones we used to have'
DELETE FROM A_ORDERS_SUPPLIERS_TO_SHARE_WITH WHERE ORDER_ID = @ID
print 'making a cursor to go through the suppliers string'
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @SUPPLIERS_TO_SHARE_WITH,', '
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Supplier = ' + @it
	INSERT INTO A_ORDERS_SUPPLIERS_TO_SHARE_WITH (ID,SUPPLIER_ID,ORDER_ID,DRCM,MODBY)
	VALUES (newID(),@it,@ID,getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs












