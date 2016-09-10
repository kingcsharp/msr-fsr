



CREATE PROCEDURE A_SP_PEOPLE_GET_SUBORDINATE_BOSS_LIST
	@strBoss nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @tester as nvarchar(50)
SELECT * FROM A_PEOPLE_HISTORY WHERE BOSS = @strBoss




