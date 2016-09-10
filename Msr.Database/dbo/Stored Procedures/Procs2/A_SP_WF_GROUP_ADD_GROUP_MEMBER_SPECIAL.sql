



CREATE  PROCEDURE A_SP_WF_GROUP_ADD_GROUP_MEMBER_SPECIAL
	@SPEC_ID nvarchar(50),
	@GROUP_ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

--check to see if it exists
if not(@GROUP_ID is null)
begin
	INSERT INTO A_WF_GROUP_SPECIALS_LINK
		(WF_GROUP_ID,SPECIAL_CODE,DRCM,MODBY)
	VALUES
		(@GROUP_ID,@SPEC_ID,getDate(),@strNTLogin)
end
else
	SELECT 'NO ID TRYING TO Insert Special ' + @SPEC_ID + ' into group ' + @GROUP_ID + '.' AS ERROR
SELECT '' AS ERROR





