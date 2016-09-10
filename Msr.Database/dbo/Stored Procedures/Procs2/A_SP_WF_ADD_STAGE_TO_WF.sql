






CREATE     PROCEDURE A_SP_WF_ADD_STAGE_TO_WF
	@STAGE_ID nvarchar(50),
	@WF_ID nvarchar(50),
	@NUM numeric,
	@strNTLogin nvarchar(50)
AS

--check to see if it exists
if not(@WF_ID is null)
begin
	INSERT INTO A_WORKFLOW_STAGE_LINK
		(WF_ID,WF_STAGE_ID,NUM,DRCM,MODBY)
	VALUES
		(@WF_ID,@STAGE_ID,@NUM,getDate(),@strNTLogin)
end
else
	SELECT 'NO ID TRYING TO Insert Stage ' + @STAGE_ID + ' into WF ' + @WF_ID + '.' AS ERROR
SELECT '' AS ERROR








