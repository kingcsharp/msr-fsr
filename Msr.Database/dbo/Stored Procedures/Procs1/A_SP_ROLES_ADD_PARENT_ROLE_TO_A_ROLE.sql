




CREATE   procedure A_SP_ROLES_ADD_PARENT_ROLE_TO_A_ROLE
	@parent as nvarchar(50),
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @myID as nvarchar(10)
exec sp_GetUniqueID3 @myID OUTPUT
INSERT INTO A_ROLE_ASSIGNEE (ID,ROLE,SOURCE,ROLE_ASSIGNED,DRCM,MODBY)
VALUES (@myID,@parent,'A_SP_ADD_PARENT',@strID,getDate(),@strNTLogin)





