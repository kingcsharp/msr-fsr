
/*
STORED PROCEDURE CALLED IN
MODULE: discussions/searchdiscussions.asp
		meeting/searchMeeting.asp
		surveys/searchSurveys.asp
		people/searchPeople.asp
*/
CREATE    PROCEDURE A_SP_ROLE_GET_BY_ID
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
SELECT ID AS ROLE_ID,NAME AS ROLE_NAME 
FROM A_APPROVED_ROLES WHERE ID = @ID
