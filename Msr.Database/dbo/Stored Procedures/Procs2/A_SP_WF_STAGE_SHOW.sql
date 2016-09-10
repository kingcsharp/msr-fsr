




CREATE   PROCEDURE A_SP_WF_STAGE_SHOW
	@STAGE_ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
SELECT @myCO = CO FROM A_V_PEOPLE_WITH_COMPANIES p WHERE PERSON = @strNTLogin
SELECT * FROM A_O_WF_STAGES WHERE ID = @STAGE_ID and CREATING_CO = @myCO




