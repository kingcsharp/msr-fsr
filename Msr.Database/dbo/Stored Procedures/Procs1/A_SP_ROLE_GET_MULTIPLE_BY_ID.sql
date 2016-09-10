


/*
STORED PROCEDURE CALLED IN
MODULE: discussions/searchdiscussions.asp
		meeting/searchMeeting.asp
		surveys/searchSurveys.asp
		people/searchPeople.asp
*/
create   PROCEDURE DBO.A_SP_ROLE_GET_MULTIPLE_BY_ID
	@ID varchar(8000),
	@strNTLogin varchar(50)
AS
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @ID,', '
UPDATE #TempItems set IT = Ltrim(IT)


SELECT ID AS ROLE_ID,NAME AS ROLE_NAME 
FROM A_APPROVED_ROLES WHERE ID in (SELECT IT FROM #TempItems)


