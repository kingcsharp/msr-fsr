








/*
STORED PROCEDURE CALLED IN survey/saveReply.asp
*/

CREATE     PROCEDURE A_SP_SURVEY_UPDATE_REF_FILES
@ID nvarchar(50),
@docID nvarchar(50),
@strNTLogin nvarchar(50)

AS
INSERT INTO A_SURVEY_ATTACHMENTS
	([SURVEY_ID],[DOC_ID], [DRCM],[MODBY])
		VALUES(@ID, @docID, getDate() ,@strNTLogin)
print 'Finished with A_SP_SURVEY_UPDATE_REF_FILES'




