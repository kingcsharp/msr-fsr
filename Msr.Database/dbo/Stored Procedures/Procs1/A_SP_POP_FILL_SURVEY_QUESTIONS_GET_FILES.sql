







/*
STORED PROCEDURE CALLED IN survey/editSurvey.asp
		        	survey/viewSurvey.asp
*/

CREATE    PROCEDURE A_SP_POP_FILL_SURVEY_QUESTIONS_GET_FILES
@responseID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM  A_V_SURVEY_REPLIES_GET_FILE_ATTACHMENT_INFO
WHERE ID=@responseID











