









CREATE        procedure A_SP_PARTS_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@strNTLogin nvarchar(50),
	@copyPrefix nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_PARTS_HISTORY (
			ID,
			COMPANY,
			COMPANY_PART_NUMBER ,
			NAME ,
			PART_TYPE ,
			SPARE ,
			CONSUMABLE ,
			UNIT ,
			UNIT_SHIPPING_WEIGHT ,
			SUPPLIER_SEE_INSTALL_BASE, 
			SUPPLIER_SEE_AVAILABILITY, 
			CUSTOMER_SEE_AVAILABILITY,
			WEIGHT_TYPE,
			DRCM, 
			MODBY)
SELECT @newID as ID,
		COMPANY,
		COMPANY_PART_NUMBER ,
		@copyPrefix + NAME ,
		PART_TYPE ,
		SPARE ,
		CONSUMABLE ,
		UNIT ,
		UNIT_SHIPPING_WEIGHT , 
		SUPPLIER_SEE_INSTALL_BASE, 
		SUPPLIER_SEE_AVAILABILITY, 
		CUSTOMER_SEE_AVAILABILITY, 
		WEIGHT_TYPE,
		gEtDate(), 
		@strNTLogin FROM A_PARTS_HISTORY WHERE ID = @strID

--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_PARTS_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID

print 'Now to copy all the subparts'
INSERT INTO A_PARTS_SUB_PARTS (ID,PARENT,PART_ID,DRCM,MODBY,QTY,NICK_NAME)
	SELECT newID(),@newID,PART_ID,DRCM,MODBY,QTY,NICK_NAME FROM A_PARTS_SUB_PARTS 
		WHERE PARENT = @strID

print 'Now to copy all stocking levels'
declare @curs as CURSOR,@it varchar(50),@newSID varchar(50)
set @curs = cursor for SELECT ID FROM A_PARTS_SAFETY_STOCK_LEVELS WHERE PART_HIST_ID = @strID
open @curs
fetch next from @curs into @it
while @@fetch_status = 0 
	begin
	exec sp_getUniqueID3 @newSID OUTPUT
	INSERT INTO A_PARTS_SAFETY_STOCK_LEVELS
		(ID,PART_ID,LOCATION_ID,MIN_LEVEL,MAX_LEVEL,DRCM,MODBY,
		CUR_LEVEL,MIN_WARNING_LEVEL,MAX_WARNING_LEVEL,PART_HIST_ID,PART_OBJ_ID,STATUS)
	SELECT @newSID,PART_ID,LOCATION_ID,MIN_LEVEL,MAX_LEVEL,getDate(),@strNTLogin,
		CUR_LEVEL,MIN_WARNING_LEVEL,MAX_WARNING_LEVEL,@newID,@newObjID,'CREATING'
		from A_PARTS_SAFETY_STOCK_LEVELS where ID = @it

	INSERT INTO A_PARTS_SAFETY_STOCK_ROLES 
		(SAFETY_STOCK_ID, ROLE_ID, EMAIL_TYPE, DRCM, MODBY, ID)
	SELECT @newSID, ROLE_ID, EMAIL_TYPE, getDate(), @strNTLogin, newID() 
		FROM A_PARTS_SAFETY_STOCK_ROLES WHERE SAFETY_STOCK_ID = @it




	fetch next from @curs into @it
	end






