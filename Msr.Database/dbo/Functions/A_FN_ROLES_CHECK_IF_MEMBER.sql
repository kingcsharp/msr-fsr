
CREATE  FUNCTION dbo.A_FN_ROLES_CHECK_IF_MEMBER(@ROLE_ID varchar(50),@P_ID varchar(50))
RETURNS smallInt
AS
BEGIN
declare @res as smallint
declare @tester varchar(50)
SELECT @tester = ID FROM A_V_ROLES_WITH_ASSIGNEES WHERE PERSON_ID = @P_ID AND ROOT = @ROLE_ID
if @tester is null set @res = 0
else set @res = 1
return(@res)
end
