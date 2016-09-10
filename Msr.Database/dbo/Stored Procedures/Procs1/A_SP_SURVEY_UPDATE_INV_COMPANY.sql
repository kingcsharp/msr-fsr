
/*
STORED PROCEDURE CALLED IN survey/saveSurvey.asp
*/

CREATE      PROCEDURE A_SP_SURVEY_UPDATE_INV_COMPANY
@surveyID nvarchar(50),
@companyID nvarchar(2000),
@strNTLogin nvarchar(50)

AS
INSERT INTO A_SURVEY_INV_COMPANY
	([SURVEY_ID],[COMPANY_ID], [DRCM],[MODBY])
		VALUES(@surveyID,@companyID, getDate() ,@strNTLogin)
print 'Finished with A_SP_SURVEY_UPDATE_INV_COMPANIES'
exec A_SP_SURVEY_UPDATE_REALLY_INVITED_COMPANY @surveyID, @companyID, @strNTLogin
