



CREATE  PROCEDURE A_SP_POPFILL_APPROVAL_PEOPLE_BY_WF_GROUP
	@GROUP_ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
SELECT @myCO = CO FROM A_V_PEOPLE_WITH_COMPANIES p WHERE PERSON = @strNTLogin
declare @strWhere nvarchar(500)
declare @sql nvarchar(500)

SELECT * FROM A_V_WF_GROUP_MEMBERS M WHERE M.GROUP_ID = @GROUP_ID ORDER BY M.PERSON_NAME




