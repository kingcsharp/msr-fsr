









/*
STORED PROCEDURE CALLED IN 

MODULE: disucssion/editSurvey.asp
*/

CREATE        PROCEDURE A_SP_POP_FILL_SURVEY_INVITED_PEOPLE
@surveyID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM  A_V_POP_FILL_SURVEY_INVITED_PEOPLE
WHERE S_ID=@surveyID







