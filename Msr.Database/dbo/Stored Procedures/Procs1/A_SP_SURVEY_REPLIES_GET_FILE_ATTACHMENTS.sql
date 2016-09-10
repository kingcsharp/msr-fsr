




/*
STORED PROCEDURE CALLED IN survey/viewFilesSurvey.asp
*/
CREATE    PROCEDURE A_SP_SURVEY_REPLIES_GET_FILE_ATTACHMENTS
@responseID nvarchar(50),
@strNTLogin nvarchar(50)
AS
SELECT * FROM A_V_SURVEY_REPLIES_GET_FILE_ATTACHMENT_INFO 
where ID = @responseID


























