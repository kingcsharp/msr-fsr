








/*
STORED PROCEDURE CALLED IN survey/saveReply.asp
*/

create     PROCEDURE A_SP_SURVEY_REPLIES_UPDATE_REF_FILES
@surveyID nvarchar(50),
@reSponseID nvarchar(50),
@docID nvarchar(50),
@strNTLogin nvarchar(50)

AS
INSERT INTO A_SURVEY_ATTACHMENTS
	([SURVEY_ID],[RESPONSE_ID], [DOC_ID], [DRCM],[MODBY])
		VALUES(@surveyID, @responseID, @docID, getDate() ,@strNTLogin)
print 'Finished with A_SP_SURVEY_REPLIES_UPDATE_REF_FILES '




