





CREATE    PROCEDURE A_SP_WF_GROUP_UPDATE_ONE
	@NAME nvarchar(50),
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

--check to see if it exists
if not(@ID is null)
begin
	print 'ID is not null'
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_WF_GROUPS WHERE ID = @ID
	if @tester is null
		select 'Error cannot find the group ' + @ID + ' to update them ' as ERROR
	else
		UPDATE A_WF_GROUPS SET
		NAME = @NAME,
		MODBY = @strNTLogin,
		DRCM = getDate()
		WHERE ID = @ID
end
else
	begin
		exec SP_GetUniqueID3 @ID OUTPUT
		INSERT INTO A_WF_GROUPS (ID,NAME,MODBY,DRCM) VALUES
		(@ID,@NAME,@strNTLogin,getDate())
	end 

SELECT @ID AS ID







