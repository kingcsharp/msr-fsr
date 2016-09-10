







/*
STORED PROCEDURE CALLED IN survey/editSurvey.asp
*/

CREATE          PROCEDURE A_SP_POP_FILL_SURVEY_INVITED_COMPANY
@surveyID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM  A_V_POP_FILL_SURVEY_INVITED_COMPANY
WHERE S_ID=@surveyID








