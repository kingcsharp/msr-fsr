


CREATE        PROCEDURE [dbo].[A_SP_LOCATIONS_UPDATE_ONE_LOCATION]
	@newObjID varchar(50) OUTPUT,
	@messages varchar(500) OUTPUT,
	@objID varchar(50),
	@NAME nvarchar(1000),
	@PARENT varchar(50),
	@ADDRESS_1 varchar(300),
	@ADDRESS_2 varchar(300),
	@CITY varchar(150),
	@STATE varchar(50),
	@COUNTRY varchar(50),
	@POSTAL_CODE varchar(50),
	@REGION varchar(50),
	@INTERNAL_ADDRESS varchar(300),
	@strNTLogin varchar(50)
as
declare @myID as nvarchar(50)
declare @regionName as nvarchar(1000)

SELECT @regionName = NAME FROM A_APPROVED_REGIONS
WHERE OBJECT_ID = @REGION


/**** Added population of address fields for children to interit from the parent ****/
if @PARENT is not null
begin
	declare @parentLocationName varchar (50)
	SELECT @parentLocationName = NAME
		,@ADDRESS_1 = ADDRESS_1
		,@ADDRESS_2 = ADDRESS_2
		,@CITY = CITY
		,@STATE = STATE
		,@COUNTRY = COUNTRY
		,@POSTAL_CODE = POSTAL_CODE
		,@REGION = REGION
		FROM A_LOCATIONS_HISTORY
	WHERE @PARENT = OBJECT_ID
end


if @objID is null
	begin
		print 'This is a new location so inserting it now'
		exec sp_GetUniqueID3 @myID OUTPUT
		INSERT INTO A_LOCATIONS_HISTORY (ID,NAME,PARENT_LOCATION,PARENT_LOCATION_NAME, ADDRESS_1,ADDRESS_2,CITY,STATE,COUNTRY,
		POSTAL_CODE,REGION,REGION_NAME,INTERNAL_ADDRESS,DRCM,MODBY)
		VALUES (@myID,@NAME,@PARENT,@parentLocationName,@ADDRESS_1,@ADDRESS_2,@CITY,@STATE,
		@COUNTRY,@POSTAL_CODE,@REGION,@regionName,@INTERNAL_ADDRESS,getDate(),@strNTLogin)
		SELECT @newObjID = OBJECT_ID FROM A_LOCATIONS_HISTORY WHERE ID = @myID 
	end
else
	begin
		print 'This is an update to the existing location ' + @myID
		UPDATE A_LOCATIONS_HISTORY set NAME = @NAME,PARENT_LOCATION = @PARENT,PARENT_LOCATION_NAME = @parentLocationName, ADDRESS_1 = @ADDRESS_1,
		ADDRESS_2 = @ADDRESS_2,CITY = @CITY,STATE = @STATE,COUNTRY = @COUNTRY,POSTAL_CODE = @POSTAL_CODE,
		REGION = @REGION,REGION_NAME= @regionName,INTERNAL_ADDRESS = @INTERNAL_ADDRESS,DRCM = getDate(),MODBY = @strNTLogin
		WHERE OBJECT_ID = @objID
		set @newObjID = @objID
	end






