
CREATE    PROCEDURE DBO.A_SP_WF_STAGE_DELETE_MEMBERS
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
declare @myCo as varchar(50)
SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
print 'My Co = ' + @myCo
--check to see if it exists
if not(@ID is null)
begin
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_WF_STAGES WHERE ID = @ID AND CREATING_CO = @myCo
	if @tester is null
		select 'Error cannot find the stage ' + @ID + ' to update it ' as ERROR
	else
		DELETE FROM A_WF_STAGE_GROUP_LINK
		WHERE WF_STAGE_ID = @ID
end
else
	SELECT 'NO ID TRYING TO DELETE' AS ERROR
SELECT '' AS ERROR

