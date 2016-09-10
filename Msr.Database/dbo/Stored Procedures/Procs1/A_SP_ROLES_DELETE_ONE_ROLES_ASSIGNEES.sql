




CREATE   procedure A_SP_ROLES_DELETE_ONE_ROLES_ASSIGNEES
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
DELETE FROM A_ROLE_ASSIGNEE WHERE ROLE = @strID





