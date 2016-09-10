


CREATE  PROCEDURE A_SP_OBJECT_CHECKED_TO_ME
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @tester as nvarchar(50)
SELECT @tester = ID FROM A_OBJECTS WHERE ID = @strID AND LOCKED_BY = @strNTLogin
if @tester is null
	select NULL as RETURN_VALUE
else
	select '1' as RETURN_VALUE




