




CREATE   procedure A_SP_ROLES_ASSIGN_ROLE_TO_A_ROLE
	@child as nvarchar(50),
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @myID as nvarchar(10)
exec sp_GetUniqueID3 @myID OUTPUT
INSERT INTO A_ROLE_ASSIGNEE (ID,ROLE,SOURCE,ROLE_ASSIGNED,DRCM,MODBY)
VALUES (@myID,@strID,'A_SP_ASSIGN_ROLE',@child,getDate(),@strNTLogin)





