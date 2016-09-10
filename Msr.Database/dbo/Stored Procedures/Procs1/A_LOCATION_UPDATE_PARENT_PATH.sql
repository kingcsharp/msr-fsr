CREATE PROCEDURE DBO.A_LOCATION_UPDATE_PARENT_PATH
@ID varchar(50)
AS
print 'Updating parent  path'
declare @pID varchar(50),@pName nvarchar(50),@curID varchar(50),@myName nvarchar(500)
declare @s nvarchar(2000)
SELECT @pID = PARENT_LOCATION,@myName = NAME FROM A_LOCATIONS_HISTORY WHERE ID = @ID
print 'My Name = ' + @myNAme
while @pID is not null
	begin
	set @curID = @pID
	SELECT @pName = NAME FROM A_V_LOCATIONS_APPROVED_DATA WHERE ID = @curID
	if @pName is not null
		begin
		print 'PArent NAme = ' + @pName
		set @s = @pName + isNull(' - ' + @s,'')
		end
	set @pID = null
	SELECT @pID = PARENT_LOCATION FROM A_V_LOCATIONS_APPROVED_DATA WHERE ID = @curID
	end
set @s = isNull(@s + ' - ','') + isNull(@myName,'')

UPDATE A_LOCATIONS_HISTORY SET COMPLETE_NAME = @s WHERE ID = @ID



