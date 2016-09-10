







CREATE      PROCEDURE A_SP_WF_ADD_ACTIVITY_TO_WF
	@ACT_ID nvarchar(50),
	@WF_ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

--check to see if it exists
if not(@WF_ID is null)
begin
	INSERT INTO A_WF_ACT_LINK
		(ID,WF_ID,ACT_ID,DRCM,MODBY)
	VALUES
		(newID(),@WF_ID,@ACT_ID,getDate(),@strNTLogin)
end
else
	SELECT 'NO ID TRYING TO Insert Activity ' + @ACT_ID + ' into WF ' + @WF_ID + '.' AS ERROR
SELECT '' AS ERROR









