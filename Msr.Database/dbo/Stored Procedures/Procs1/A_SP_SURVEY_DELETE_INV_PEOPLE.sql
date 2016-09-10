



/*
STORED PROCEDURE CALLED IN survey/editSurvey.asp
*/







CREATE    PROCEDURE A_SP_SURVEY_DELETE_INV_PEOPLE 
@surveyID nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_SURVEY_INV_PEOPLE WHERE 
SURVEY_ID = @surveyID










