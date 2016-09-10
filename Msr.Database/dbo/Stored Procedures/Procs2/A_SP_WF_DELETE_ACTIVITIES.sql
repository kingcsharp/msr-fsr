







CREATE      PROCEDURE A_SP_WF_DELETE_ACTIVITIES
	@WF_ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

--check to see if it exists
if not(@WF_ID is null)
begin
	DELETE FROM A_WF_ACT_LINK WHERE WF_ID = @WF_ID
end
else
	PRINT 'NO ID TRYING TO DELETE Activity of  WF ' + @WF_ID + '.'
SELECT '' AS ERROR









