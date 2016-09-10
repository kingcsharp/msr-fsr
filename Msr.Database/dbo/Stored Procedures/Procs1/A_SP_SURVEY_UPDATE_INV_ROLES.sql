





/*
STORED PROCEDURE CALLED IN survey/editSurvey.asp
*/

CREATE   PROCEDURE A_SP_SURVEY_UPDATE_INV_ROLES
@surveyID nvarchar(50),
@roleID nvarchar(2000),
@strNTLogin nvarchar(50)

AS
INSERT INTO A_SURVEY_INV_ROLE
	([SURVEY_ID],[ROLE_ID], [DRCM],[MODBY])
		VALUES(@surveyID,@roleID, getDate() ,@strNTLogin)
print 'Finished with A_SP_SURVEY_UPDATE_INV_ROLES'








