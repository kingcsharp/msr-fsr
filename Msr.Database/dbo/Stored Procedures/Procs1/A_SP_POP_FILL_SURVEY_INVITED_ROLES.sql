






/*
STORED PROCEDURE CALLED IN disucssion/editSurvey.asp
*/

CREATE         PROCEDURE A_SP_POP_FILL_SURVEY_INVITED_ROLES
@surveyID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM  A_V_POP_FILL_SURVEY_INVITED_ROLES
WHERE S_ID=@surveyID







