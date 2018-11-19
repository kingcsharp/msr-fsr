




CREATE   procedure A_SP_ROLES_ASSIGN_PERSON_TO_A_ROLE
	@child as nvarchar(50),
	@strID nvarchar(50),
	@StartDate datetime,
	@EndDate datetime,
	@strNTLogin nvarchar(50)
as
declare @myID as nvarchar(10)
exec sp_GetUniqueID3 @myID OUTPUT
INSERT INTO A_ROLE_ASSIGNEE (ID,ROLE,SOURCE,PERSON,DRCM,MODBY,StartDate,EndDate)
VALUES (@myID,@strID,'A_SP_ASSIGN_ROLE',@child,getDate(),@strNTLogin,@StartDate,@EndDate)





