



CREATE  PROCEDURE A_SP_WF_GROUP_ADD_GROUP_MEMBER
	@PERSON_ID nvarchar(50),
	@GROUP_ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

--check to see if it exists
if not(@GROUP_ID is null)
begin
	INSERT INTO A_WF_GROUP_PEOPLE_LINK
		(WF_GROUP_ID,USER_ID,DRCM,MODBY)
	VALUES
		(@GROUP_ID,@PERSON_ID,getDate(),@strNTLogin)
end
else
	SELECT 'NO ID TRYING TO Insert Person ' + @PERSON_ID + ' into group ' + @GROUP_ID + '.' AS ERROR
SELECT '' AS ERROR





