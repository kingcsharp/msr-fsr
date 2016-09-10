




CREATE   PROCEDURE A_SP_WF_STAGE_ADD_STAGE_GROUP
	@GROUP_ID nvarchar(50),
	@STAGE_ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

--check to see if it exists
if not(@STAGE_ID is null)
begin
	INSERT INTO A_WF_STAGE_GROUP_LINK
		(WF_STAGE_ID,WF_GROUP_ID,DRCM,MODBY)
	VALUES
		(@STAGE_ID,@GROUP_ID,getDate(),@strNTLogin)
end
else
	SELECT 'NO ID TRYING TO Insert Group ' + @GROUP_ID + ' into stage ' + @STAGE_ID + '.' AS ERROR
SELECT '' AS ERROR






