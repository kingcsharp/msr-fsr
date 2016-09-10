/*
STORED PROCEDURE CALLED IN
MODULE: discussions/searchdiscussions.asp
		meeting/searchMeeting.asp
		surveys/searchSurveys.asp
		people/searchPeople.asp
*/
CREATE     PROCEDURE A_SP_COMPANY_GET_BY_ID
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
SELECT ID AS CO_ID,NAME AS CO_NAME 
FROM A_APPROVED_COMPANIES WHERE ID = @ID
