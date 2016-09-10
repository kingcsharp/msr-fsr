




CREATE    PROCEDURE A_SP_WF_DELETE_STAGES
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

--check to see if it exists
if not(@ID is null)
begin
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_WORKFLOWS WHERE ID = @ID AND CREATING_CO = (select dbo.getCompany(@strNTLogin))
	if @tester is null
		select 'Error cannot find the stage ' + @ID + ' to update it ' as ERROR
	else
		DELETE FROM A_WORKFLOW_STAGE_LINK
		WHERE WF_ID = @ID
end
else
	SELECT 'NO ID TRYING TO DELETE' AS ERROR
SELECT '' AS ERROR







