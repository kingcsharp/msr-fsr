




/*
STORED PROCEDURE CALLED IN disucssion/editSurvey.asp
*/





CREATE     PROCEDURE A_SP_SURVEY_DELETE_INV_ROLES 
@surveyID nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_SURVEY_INV_ROLE WHERE 
SURVEY_ID = @surveyID











