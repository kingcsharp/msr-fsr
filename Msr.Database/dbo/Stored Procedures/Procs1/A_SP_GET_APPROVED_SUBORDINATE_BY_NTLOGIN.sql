





CREATE   PROCEDURE A_SP_GET_APPROVED_SUBORDINATE_BY_NTLOGIN
	@strBoss nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @tester as nvarchar(50)
SELECT * FROM A_V_PEOPLE_BOSS_SUB_LOOKUP WHERE BOSS_ID = @strBoss






