



CREATE  PROCEDURE A_SP_WF_GROUP_DELETE_MEMBERS
	@ID nvarchar(50)
AS

--check to see if it exists
if not(@ID is null)
begin
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_WF_GROUPS WHERE ID = @ID
	if @tester is null
		select 'Error cannot find the group ' + @ID + ' to update them ' as ERROR
	else
		DELETE FROM A_WF_GROUP_PEOPLE_LINK
		WHERE WF_GROUP_ID = @ID
		DELETE FROM A_WF_GROUP_ROLE_LINK
		WHERE WF_GROUP_ID = @ID
		DELETE FROM A_WF_GROUP_SPECIALS_LINK
		WHERE WF_GROUP_ID = @ID
end
else
	SELECT 'NO ID TRYING TO DELETE' AS ERROR
SELECT '' AS ERROR





