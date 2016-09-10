


CREATE   PROCEDURE A_SP_OBJECT_CHECKED_TO_SUBORDINATE
	@strID nvarchar(50),
	@boss nvarchar(50)
as
declare @tester as nvarchar(50)
SELECT @tester = b.ID FROM A_V_OBJECTS_WITH_LOCKED_BY_BOSS b WHERE ID = @strID AND BOSS_ID in (SELECT SUBORDINATE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS =  @boss)
if @tester is null
	select NULL as RETURN_VALUE
else
	select '1' as RETURN_VALUE




