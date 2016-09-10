



CREATE  PROCEDURE A_SP_SPECIAL_APPROVERS_SEARCH
	@NAME nvarchar(50),
	@strNTLogin nvarchar(50)
AS

--Get my company
declare @myCo as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCo OUTPUT


--if we dont have people then just do a simple role search and save time
if @NAME is null 
	SELECT * FROM A_WF_GROUP_SPECIAL_MEMBERS ORDER BY NAME
else
	SELECT * FROM A_WF_GROUP_SPECIAL_MEMBERS WHERE NAME like '%' + @NAME + '%' ORDER BY NAME






