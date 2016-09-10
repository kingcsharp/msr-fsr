





/*
STORED PROCEDURE CALLED IN survey/searchSurvey.asp
*/

CREATE  PROCEDURE A_SP_SURVEY_CLOSE
@surveyID nvarchar(50),
@strNTLogin nvarchar(50)

AS
UPDATE A_SURVEYS SET STATUS = 'CLOSED' WHERE ID =@surveyID  







