

CREATE  PROCEDURE dbo.A_SP_ADMIN_EDIT_ENGINEER_SCREEN_REPORT
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID nvarchar(50),
@FILE_ID varchar(50),
@DATE_ADDED datetime,
@DATE_REMOVED dateTime,
@NAME varchar(2000),
@DESCRIPTION varchar(2000),
@strNTLogin varchar(50)
AS
declare @ROOT_CO varchar(50)
SELECT @ROOT_CO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
print 'Updating a Engineer Screen File Link'
if @ID is null
	begin
		print 'ID is Null we need to create this link'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_ENGINEER_SCREEN_REPORTS(ID,NAME,MODBY,DRCM,ROOT_COMPANY,DATE_ADDED,ADDED_BY) 
			VALUES(@ID,@NAME,@strNTLogin,getDATE(),@ROOT_CO,getDAte(),@strNTLogin)
	end
else
	begin
		print 'The ID is not null so we are just updating'
		set @newID = @ID
	end

if @DATE_ADDED is null
	SELECT @DATE_ADDED = DATE_ADDED FROM A_ENGINEER_SCREEN_REPORTS WHERE ID = @ID
else
	SELECT @DATE_ADDED = dbo.timeToGrenich(@DATE_ADDED,@strNTLogin)

if @DATE_REMOVED is NOT null
	SELECT @DATE_REMOVED = dbo.timeToGrenich(@DATE_REMOVED,@strNTLogin)

UPDATE A_ENGINEER_SCREEN_REPORTS SET 
	FILE_ID = @FILE_ID,
	DATE_ADDED = @DATE_ADDED,
	DATE_REMOVED = @DATE_REMOVED,
	DRCM = getDate(),
	NAME = @NAME,
	DESCRIPTION = @DESCRIPTION
WHERE ID = @ID




