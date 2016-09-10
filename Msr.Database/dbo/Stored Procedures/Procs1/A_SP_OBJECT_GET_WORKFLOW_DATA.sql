




CREATE      PROCEDURE A_SP_OBJECT_GET_WORKFLOW_DATA
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
SELECT o.* FROM A_V_WF_STARTED_WITH_DENIAL_INFO o where o.OBJECT_ID = @strID



