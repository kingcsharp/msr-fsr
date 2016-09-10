






CREATE      PROCEDURE A_SP_WF_STAGE_UPDATE_ONE
	@NAME nvarchar(50),
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

declare @myCo as varchar(50)
SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
print 'My Co = ' + @myCo
--check to see if it exists
if not(@ID is null)
begin
	print 'ID is not null'
	declare @tester as nvarchar(50)
 	SELECT @tester = ID FROM A_O_WF_STAGES WHERE ID = @ID AND CREATING_CO = @myCo
 	if @tester is null
 		select 'Error cannot find the stage ' + @ID + ' to update them ' as ERROR
 	else
 		UPDATE A_WF_STAGES SET
 		NAME = @NAME,
 		MODBY = @strNTLogin,
 		DRCM = getDate()
 		WHERE ID = @ID
end
 else
 	begin
		exec sp_getUniqueID3 @ID OUTPUT
		INSERT INTO A_WF_STAGES (ID,NAME,MODBY,DRCM) VALUES
		(@ID,@NAME,@strNTLogin,getDate())
	end

SELECT @ID as ID









