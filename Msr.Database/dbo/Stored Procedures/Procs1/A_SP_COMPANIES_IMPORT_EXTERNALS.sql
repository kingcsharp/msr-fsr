





CREATE       PROCEDURE dbo.A_SP_COMPANIES_IMPORT_EXTERNALS
@newID varchar(50) OUTPUT,
@msgs varchar(2000) OUTPUT,
@ID varchar(2000),
@NAME varchar(2000),
@ADDRESS varchar(2000),
@CITY varchar(2000),
@STATE varchar(2000),
@ZIP varchar(2000),
@COUNTRY varchar(2000),
@SHIP_NAME varchar(2000),
@SHIP_ADDRESS varchar(2000),
@SHIP_CITY varchar(2000),
@SHIP_STATE varchar(2000),
@SHIP_ZIP varchar(2000),
@SHIP_COUNTRY varchar(2000),
@PHONE varchar(2000),
@SUPPLIER_ID varchar(2000),
@LINKED_ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Starting import of a company'
set @LINKED_ID = isNull(@LINKED_ID,@strNTLogin)
if @LINKED_ID is null 
	goto fin
declare @ADMIN_LOGIN varchar(50),@ADMIN_PASSWORD varchar(50),@OE_LOGIN varchar(50),@OE_PASSWORD varchar(50),
	@impID varchar(50)
set @impID = @SUPPLIER_ID+'__'+@ID
set @ADMIN_LOGIN = @ID + 'Admin'
set @OE_LOGIN = @ID + 'OE' + @strNTLogin
exec sp_GetUniqueID3 @ADMIN_PASSWORD OUTPUT
exec sp_GetUniqueID3 @OE_PASSWORD OUTPUT

if not exists (SELECT * FROM A_COMPANIES_IMPORT WHERE ID = @impID)
INSERT INTO A_COMPANIES_IMPORT
(ID,EXTERNAL_ID, NAME, ADDRESS, CITY, STATE, ZIP, COUNTRY, SHIP_NAME, SHIP_ADDRESS, 
SHIP_CITY, SHIP_STATE, SHIP_ZIP, SHIP_COUNTRY, PHONE, SUPPLIER, 
ADMIN_LOGIN, ADMIN_PASSWORD, OE_LOGIN, OE_PASSWORD)
VALUES(
@impID,@ID, @NAME, @ADDRESS, @CITY, @STATE, @ZIP, @COUNTRY, @SHIP_NAME, @SHIP_ADDRESS, 
@SHIP_CITY, @SHIP_STATE, @SHIP_ZIP, @SHIP_COUNTRY, @PHONE, @SUPPLIER_ID, 
@ADMIN_LOGIN, @ADMIN_PASSWORD, @OE_LOGIN, @OE_PASSWORD
)
else
	begin
	print 'Already got this one so just updating it'
	UPDATE A_COMPANIES_IMPORT SET
	NAME = @NAME, 
	ADDRESS = @ADDRESS, 
	CITY = @CITY, 
	STATE = @STATE, 
	ZIP = @ZIP, 
	COUNTRY = @COUNTRY, 
	SHIP_NAME = @SHIP_NAME, 
	SHIP_ADDRESS = @SHIP_ADDRESS, 
	SHIP_CITY = @SHIP_CITY, 
	SHIP_STATE = @SHIP_STATE, 
	SHIP_ZIP = @SHIP_ZIP, 
	SHIP_COUNTRY = @SHIP_COUNTRY, 
	PHONE = @PHONE
	WHERE ID = @impID
	SELECT 	@SUPPLIER_ID = SUPPLIER,
		@ADMIN_LOGIN = ADMIN_LOGIN,@ADMIN_PASSWORD = ADMIN_PASSWORD
		FROM A_COMPANIES_IMPORT WHERE ID = @impID 
	end
	
declare @coObjId varchar(50),@coHistID varchar(50),@fname varchar(50),@lname varchar(50),@coRootID varchar(50)
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin

exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
@coRootID OUTPUT, -- varchar(50) OUTPUT,
@coHistID OUTPUT, -- varchar(50) OUTPUT,
@coObjID OUTPUT, -- varchar(50) OUTPUT,
@ID, -- varchar(50),
'A_COMPANIES_HISTORY',-- varchar(50),
@strNTLogin --varchar(50)

if @coRootID is null
	begin
	print 'This is a new one so we have to create it'
	set @fname = @ID
	set @lName = 'ADMIN'
	exec A_SP_COMPANIES_UPDATE_ONE_COMPANY
	@coObjID OUTPUT,   -- nvarchar(50) OUTPUT,
	null,
	null,
	@NAME, --@NAME nvarchar(100),
	'COMPANY', --@TYPE nvarchar(50),
	NULL, --@PARENT_COMPANY nvarchar(50),
	@PHONE, -- nvarchar(50),
	null, --@HEAD_PEOPLE varchar(8000),
	null, --@LOCATION_ID varchar(50),
	null, --@CO_SUP_PRODS varchar(8000),
	null, --@PIC_FILES varchar(8000),
	null, --@LOGO_FILES varchar(8000),
	null, --@REFERENCE_FILES varchar(8000),
	@ADMIN_LOGIN, --@NEW_PERSON_LOGIN nvarchar(200),
	@ADMIN_PASSWORD, --@NEW_PERSON_PASSWORD nvarchar(50),
	null, --@NEW_PERSON_EMAIL nvarchar(200),
	@fname, --@NEW_PERSON_FIRST_NAME nvarchar(200),
	@lname, --@NEW_PERSON_LAST_NAME nvarchar(200),
	@strNTLogin --nvarchar(50)
	
	SELECT @coRootID = @coObjID, @coHistID = ID FROM A_COMPANIES_HISTORY WHERE OBJECT_ID = @coObjID
	exec A_SP_OBJECTS_QUICK_APPROVE @coObjID,@strNTLogin
	exec A_SP_COMPANIES_FINISH_WF @coHistID,@coObjID,@strNTLogin
	INSERT INTO A_OBJECT_EXTERNAL_REF 
			(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,DRCM,MODBY)
		VALUES
			(newID(),@coHistID,@coObjID,'A_COMPANIES_HISTORY',
				@rootCo + '___' + @ID,getDAte(),@strNTLogin)
	end
else
	begin
	print 'THIS IS AN UPDATE TO AN ALREADY EXISTING COMPANY'
	UPDATE A_COMPANIES_HISTORY SET NAME = @NAME,PHONE = @PHONE WHERE ID = @coHistID
	end

UPDATE A_COMPANIES_IMPORT SET HISTORY_ID = @coHistID,OBJECT_ID = @coObjID,ROOT_ID = @coRootID WHERE ID = @impID
print 'Company and admin made'
declare @adminID varchar(50)
SELECT @adminID = ID FROM A_V_PEOPLE_APPROVED_DATA WHERE LOGIN = @ADMIN_LOGIN
if @adminID = null
	begin
	print 'Could not find the admin for company = ' + @coRootID
	goto fin
	end
declare @retPass varchar(50)
exec A_SP_UTIL_GET_ENCRYPTED_PASSWORD @retPass OUTPUT,@ADMIN_PASSWORD
UPDATE A_PEOPLE_HISTORY SET PASSWORD = @retPass WHERE OBJECT_ID = @adminID

declare @intLocHistID varchar(50),@intLocObjID varchar(50),@intLocRootID varchar(50),@locID varchar(50)
print 'now to make the main address'
if @address is not null
	begin
	print 'CREATING A LOCATION FOR THE MAIN ADDRESS'
	set @locID = @SUPPLIER_ID + '_' + @ID + '_MAIN_LOCATION'
	exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
	@intLocRootID OUTPUT, -- varchar(50) OUTPUT,
	@intLocHistID OUTPUT, -- varchar(50) OUTPUT,
	@intLocObjID OUTPUT, -- varchar(50) OUTPUT,
	@locID, -- varchar(50),
	'A_LOCATIONS_HISTORY',-- varchar(50),
	@adminID --varchar(50)
	if @intLocRootID is null
		begin
		print 'LOCATION IS NEW MAKING IT NOW ' + @locID
		exec A_SP_LOCATIONS_UPDATE_ONE_LOCATION
		@intLocObjID OUTPUT,
		null,
		null, --@objID varchar(50),
		@NAME, -- nvarchar(1000),
		null, --@PARENT varchar(50),
		@ADDRESS, -- varchar(300),
		null,--@ADDRESS_2 varchar(300),
		@CITY, -- varchar(150),
		@STATE, -- varchar(50),
		@COUNTRY, -- varchar(50),
		@ZIP, -- varchar(50),
		null, --@REGION varchar(50),
		null, --@INTERNAL_ADDRESS varchar(300),
		@adminID -- varchar(50)
		SELECT @intLocRootID = @intLocObjID, @intLocHistID = ID FROM A_LOCATIONS_HISTORY WHERE OBJECT_ID = @intLocObjID
		exec A_SP_OBJECTS_QUICK_APPROVE @intLocObjID,@adminID
		exec A_SP_LOCATIONS_FINISH_WF @intLocHistID,@intLocObjID,@adminID
		INSERT INTO A_OBJECT_EXTERNAL_REF 
				(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,DRCM,MODBY)
		VALUES	(newID(),@intLocHistID,@intLocObjID,'A_LOCATIONS_HISTORY',@coRootID + '___' + @locID ,getDAte(),@adminID)
		end
	UPDATE A_COMPANIES_HISTORY SET LOCATION = @intLocRootID WHERE ID = @coHistID
	end	

if @SHIP_ADDRESS is not null
	begin
	select @intLocRootID = null,@intLocHistID = null,@intLocObjID = null
	print 'CREATING A SHIPPING ADDRESS'
	set @locID = @SUPPLIER_ID + '_' + @ID + '_SHIPPING_LOCATION'
	exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
	@intLocRootID OUTPUT, -- varchar(50) OUTPUT,
	@intLocHistID OUTPUT, -- varchar(50) OUTPUT,
	@intLocObjID OUTPUT, -- varchar(50) OUTPUT,
	@locID, -- varchar(50),
	'A_LOCATIONS_HISTORY',-- varchar(50),
	@adminID --varchar(50)
	if @intLocRootID is null
		begin
		print 'SHIPPING LOCATION IS NEW MAKING IT NOW ' + @locID
		exec A_SP_LOCATIONS_UPDATE_ONE_LOCATION
		@intLocObjID OUTPUT,
		null,
		null, --@objID varchar(50),
		@SHIP_NAME, -- nvarchar(1000),
		null, --@PARENT varchar(50),
		@SHIP_ADDRESS, -- varchar(300),
		null,--@ADDRESS_2 varchar(300),
		@SHIP_CITY, -- varchar(150),
		@SHIP_STATE, -- varchar(50),
		@SHIP_COUNTRY, -- varchar(50),
		@SHIP_ZIP, -- varchar(50),
		null, --@REGION varchar(50),
		null, --@INTERNAL_ADDRESS varchar(300),
		@adminID -- varchar(50)
		SELECT @intLocRootID = @intLocObjID, @intLocHistID = ID FROM A_LOCATIONS_HISTORY WHERE OBJECT_ID = @intLocObjID
		exec A_SP_OBJECTS_QUICK_APPROVE @intLocObjID,@adminID
		exec A_SP_LOCATIONS_FINISH_WF @intLocHistID,@intLocObjID,@adminID
		INSERT INTO A_OBJECT_EXTERNAL_REF 
				(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,DRCM,MODBY)
		VALUES	(newID(),@intLocHistID,@intLocObjID,'A_LOCATIONS_HISTORY',@coRootID + '___' + @locID ,getDAte(),@adminID)
		end
	end	


declare @oeHistID varchar(50),@oeObjID varchar(50),@oeRootID varchar(50),@oeID varchar(50)
print 'now check if this person already has an OE for this company'
declare @firName varchar(200),@laName varchar(500)
if not exists (SELECT ID FROM A_COMPANIES_IMPORT_OES WHERE IMPORT_CO_ID = @impID and IMPORTER = @strNTLogin)
	begin
	SELECT @firName = NAME, @laName = LAST_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
	exec A_SP_UTIL_GET_ENCRYPTED_PASSWORD @retPass OUTPUT,@OE_PASSWORD		
	print 'OE PERSON IS NEW MAKING IT NOW ' + isnull(@oeID,'NULL')
	print 'First name:' + @firName
	print 'Last name:' + @laName
	exec A_SP_PEOPLE_UPDATE_ONE_PERSON
		@oeObjID OUTPUT,
		null, -- varchar(500) OUTPUT,
		null, -- @objID varchar(50),
		@OE_LOGIN,-- nvarchar(50),
		@firName, -- nvarchar(100),
		@retPass, -- nvarchar(50),
		@laName, -- nvarchar(100),
		'en', --@LANG varchar(50),
		@coRootID, -- varchar(50),
		null, --@POSITION varchar(50),
		null, --@BOSS varchar(50),
		'81', --@TIME_ZONE varchar(50),
		null, --@HIRE_DATE datetime,
		'ACTIVE', --@STATUS varchar(50),
		'ORDER_ENTRY_SCREEN', --OE@SCREEN_TYPE varchar(50),
		'0', --@IS_HEAD varchar(5),
		@adminID -- varchar(50)
	SELECT @oeRootID = @oeObjID, @oeHistID = ID FROM A_PEOPLE_HISTORY WHERE OBJECT_ID = @oeObjID
	exec A_SP_OBJECTS_QUICK_APPROVE @oeObjID,@adminID
	exec A_SP_PEOPLE_FINISH_APPROVAL_WF @oeHistID,@adminID
	INSERT INTO A_COMPANIES_IMPORT_OES
			(ID,IMPORT_CO_ID,OE_LOGIN,OE_PASSWORD,IMPORTER,DRCM)
	VALUES	(newID(),@impID,@OE_LOGIN,@retPass,@strNTLogin,getDate())
	end
else
	print ' This person alread exists'



if @OE_PASSWORD IS NULL
	begin
	exec sp_GetUniqueID3 @OE_PASSWORD OUTPUT
	UPDATE A_COMPANIES_IMPORT SET OE_PASSWORD = @OE_PASSWORD WHERE ID = @impID
	end
declare @newPass varchar(50)
print 'GETTING AN ENCRYPTED PW FOR THE OE DUDE'
exec A_SP_UTIL_GET_ENCRYPTED_PASSWORD @newPass OUTPUT,@OE_PASSWORD
print 'New Password = ' + @newPass + ' ' + @oeHistID
UPDATE A_PEOPLE_HISTORY SET PASSWORD = @newPass WHERE LOGIN = @OE_LOGIN
UPDATE A_PEOPLE_HISTORY SET CHANGE_PASS = 0 WHERE LOGIN = @OE_LOGIN


print 'We should link the new user to the old user in the Linked ID column if they are not linked already'
if not exists (SELECT * FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @oeRootID)
	print '^^^^^^^^^^^^^^^^^CAN NO TFIND THE OE PERSON ' + @oeObjID
else
	begin
	DELETE FROM A_PEOPLE_LINKED_LOGINS WHERE LINKED_ID = @oeRootID and ROOT_ID = @LINKED_ID
	INSERT INTO A_PEOPLE_LINKED_LOGINS (ID,ROOT_ID,LINKED_ID,NICK_NAME,DRCM,MODBY)
	VALUES (newID(),@LINKED_ID,@oeRootID,isNull(('['+ @ID +']'),'') + isNull(@NAME,'') + @firName + ' ' + @laName,getDate(),@strNTLogin)
	end

print 'We should also make the order entry person part of the admin workflow'
declare @wfGroupID varchar(50)
SELECT top 1 @wfGroupID = g.ID FROM A_WF_GROUPS g,A_OBJECTS o 
	WHERE g.OBJECT_ID = o.ID AND CREATING_CO = @coRootID
DELETE FROM A_WF_GROUP_PEOPLE_LINK WHERE WF_GROUP_ID = @wfGroupID AND USER_ID = @oeRootID
INSERT INTO A_WF_GROUP_PEOPLE_LINK (WF_GROUP_ID,USER_ID,DRCM,MODBY)
	values (@wfGroupID,@oeRootID,getDate(),@adminID)


fin:






