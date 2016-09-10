



/*
STORED PROCEDURE CALLED IN disucssion/editSurvey.asp
*/
CREATE     PROCEDURE A_SP_SURVEY_UPDATE_INV_PEOPLE
@surveyID nvarchar(50),
@peopleID nvarchar(2000),
@strNTLogin nvarchar(50)

AS
INSERT INTO A_SURVEY_INV_PEOPLE
	([SURVEY_ID],[PEOPLE_ID], [DRCM],[MODBY])
		VALUES(@surveyID,@peopleID, getDate() ,@strNTLogin)
print 'Finished with A_SP_SURVEY_UPDATE_INV_PEOPLE'




