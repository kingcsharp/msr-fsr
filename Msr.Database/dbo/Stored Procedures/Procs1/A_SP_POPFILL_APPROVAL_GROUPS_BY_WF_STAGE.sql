


CREATE PROCEDURE A_SP_POPFILL_APPROVAL_GROUPS_BY_WF_STAGE
	@STAGE_ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
SELECT @myCO = CO FROM A_V_PEOPLE_WITH_COMPANIES p WHERE PERSON = @strNTLogin
declare @strWhere nvarchar(500)
declare @sql nvarchar(500)

SELECT * FROM A_V_WF_STAGES_WITH_GROUPS WHERE STAGE_ID = @STAGE_ID AND CREATING_CO = @myCO




