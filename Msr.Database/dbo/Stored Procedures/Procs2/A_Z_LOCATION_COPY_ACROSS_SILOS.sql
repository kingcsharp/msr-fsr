CREATE PROCEDURE dbo.A_Z_LOCATION_COPY_ACROSS_SILOS 
@newLocID varchar(50) OUTPUT,
@locID varchar(50),
@toCompanyID varchar(50)
AS

print 'Copying the Location tree ' + @locID
print 'to company = ' + @toCompanyID
declare @adminID varchar(50)
exec A_SP_COMPANY_GET_AN_ADMIN_ID_FROM_THIS_COMPANY
	@adminID OUTPUT,@toCompanyID
print 'The admin we are using is ' + @adminID
declare @adminName varchar(100)
SELECT @adminName = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @adminID
print 'His Name is ' + @adminName
declare 	
	@NAME nvarchar(1000),
	@ADDRESS_1 varchar(300),
	@ADDRESS_2 varchar(300),
	@CITY varchar(150),
	@STATE varchar(50),
	@COUNTRY varchar(50),
	@POSTAL_CODE varchar(50),
	@REGION varchar(50)
SELECT 	@NAME = isNULL(COMPLETE_NAME,NAME),@ADDRESS_1 = ADDRESS_1,@ADDRESS_2 = ADDRESS_2,
	@CITY = CITY,@STATE = STATE,@COUNTRY = COUNTRY,	@POSTAL_CODE =POSTAL_CODE,
	@REGION = REGION FROM A_V_LOCATIONS_APPROVED_DATA WHERE ID = @locID
declare @newObjID varchar(50),@msgs nvarchar(500)
exec A_SP_LOCATIONS_UPDATE_ONE_LOCATION
	@newObjID OUTPUT,@msgs OUTPUT,
	null,
	@NAME , NULL,
	@ADDRESS_1, @ADDRESS_2, 
	@CITY,@STATE,@COUNTRY,@POSTAL_CODE,@REGION,
	null,@adminID
print 'Created a location and the new object ID is ' + @newObjID
UPDATE A_OBJECTS SET STATUS = 'APPROVED',LOCKED_BY = NULL, 
	LOCKED_BY_NAME = NULL 
	WHERE ID = @newObjID
declare @newID varchar(50)
SELECT @newID = OBJ_ID FROM A_OBJECTS WHERE ID = @newObjID
exec A_SP_LOCATIONS_FINISH_WF @newID,@newObjID,@adminID
select @newLocID = ROOT FROM A_OBJECTS WHERE ID = @newObjID
INSERT INTO A_LOCATIONS_RELATED_LOCATIONS (LOCATION_ID,RELATED_ID,DRCM,MODBY)
VALUES (@newLocID,@locID,getDate(),@adminID)






