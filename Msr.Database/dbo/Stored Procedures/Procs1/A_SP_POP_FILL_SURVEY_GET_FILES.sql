






/*
STORED PROCEDURE CALLED IN survey/editSurvey.asp
		        	survey/viewSurvey.asp
*/

CREATE      PROCEDURE A_SP_POP_FILL_SURVEY_GET_FILES
@surveyID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM  A_V_SURVEY_GET_FILE_ATTACHMENTS
WHERE ID=@surveyID










