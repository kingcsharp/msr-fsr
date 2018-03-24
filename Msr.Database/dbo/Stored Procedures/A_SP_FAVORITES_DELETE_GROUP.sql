





CREATE PROCEDURE A_SP_FAVORITES_DELETE_GROUP
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

--check to see if it exists
if not(@ID is null)
begin
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_PEOPLES_FAVORITE_GROUPS WHERE ID = @ID AND PERSON = @strNTLogin
	if @tester is null
		select 'Error cannot find the favorite group ' + @ID + ' to update it ' as ERROR
	else
		begin
			DELETE FROM A_PEOPLES_FAVORITES WHERE [GROUP] = @ID
			DELETE FROM A_PEOPLES_FAVORITE_GROUPS WHERE ID = @ID
			SELECT NULL AS ERROR
		end
end
else
	SELECT 'NO ID TRYING TO DELETE' AS ERROR