




CREATE   PROCEDURE A_SP_POPFILL_APPROVAL_STAGES_BY_WORKFLOW
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
SELECT @myCO = CO FROM A_V_PEOPLE_WITH_COMPANIES p WHERE PERSON = @strNTLogin
declare @strWhere nvarchar(500)
declare @sql nvarchar(500)

SELECT * FROM A_V_WORKFLOWS_WITH_STAGES WHERE WF_ID = @ID AND isNull(STAGE_HIDE,0)= 0 ORDER BY NUM





