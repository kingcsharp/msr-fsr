


create   PROCEDURE A_SP_PEOPLE_GET_MY_BOSS_DATA
@bossID nvarchar(50),
@strNTLogin nvarchar(50)
as
SELECT * FROM A_V_PEOPLE_APPROVED_DATA
WHERE ID = @bossID

