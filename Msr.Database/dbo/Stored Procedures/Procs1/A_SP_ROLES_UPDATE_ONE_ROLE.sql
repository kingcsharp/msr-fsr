
CREATE         PROCEDURE A_SP_ROLES_UPDATE_ONE_ROLE
	@returnID nvarchar(50) OUTPUT,
	@msg nvarchar(500) OUTPUT,	
	@NAME nvarchar(100),
	@ID nvarchar(50),
	@SECURITY_LEVEL nvarchar(50),
	@strNTLogin nvarchar(50)
AS
if not(@ID is null)
begin
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_ROLES WHERE ID = @ID AND CREATING_CO = (select dbo.getCompany(@strNTLogin))
	if @tester is null
		set @msg =  'Error cannot find the ROLE ' + @ID + ' to update IT '
	else
		begin 
			UPDATE A_ROLES_HISTORY SET
			NAME = @NAME,
			SECURITY_LEVEL = @SECURITY_LEVEL,
			MODBY = @strNTLogin,
			DRCM = getDate()
			WHERE ID = @ID
			set @returnID = @ID
		end
end
else
	begin
		exec sp_getUniqueID3 @ID OUTPUT
		INSERT INTO A_ROLES_HISTORY (ID,NAME,SOURCE,HIDDEN,SECURITY_LEVEL, MODBY,DRCM) VALUES
		(@ID,@NAME,'A_SP_UPDATE_ROLE',0,@SECURITY_LEVEL,@strNTLogin,getDate())
		set @returnID = @ID
	end





