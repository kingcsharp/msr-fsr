







CREATE     procedure A_SP_COMPANIES_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@strNTLogin nvarchar(50),
	@copyPrefix nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_COMPANIES_HISTORY (
ID,NAME,CO_TYPE,PARENT,PHONE,LOCATION,LOCATION_NAME,PARENT_NAME,DRCM,MODBY,ROOT_CO_ID)
SELECT @newID as ID,@copyPrefix + NAME as NAME,CO_TYPE,PARENT,PHONE,LOCATION,LOCATION_NAME,
PARENT_NAME,DRCM,@strNTLogin,ROOT_CO_ID FROM A_COMPANIES_HISTORY WHERE ID = @strID
--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_COMPANIES_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID

print 'Now copying all my holidays'
INSERT INTO A_COMPANY_HOLIDAYS (ID,DESCRIPTION,YR,MO,DA,CO_ID,DRCM,MODBY)
SELECT newID(),DESCRIPTION,YR,MO,DA,@newID,getDate(),
@strNTLogin FROM A_COMPANY_HOLIDAYS WHERE CO_ID = @strID







