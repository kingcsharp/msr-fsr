


/*
STORED PROCEDURE CALLED IN surveys/viewSurvey

*/
CREATE      PROCEDURE A_SP_SURVEY_DELETE_OWNER_FROM_RESPONSE_ALERTS_TABLE
@surveyID varchar(50),
@strNTLogin varchar(50)
AS
print 'delete me from A_SURVEYS_RESPONSE_ALERTS because I just view the reponse'
DELETE 
FROM A_SURVEY_RESPONSE_ALERTS 
WHERE SURVEY_ID=@surveyID AND ALERTEE_ID = @strNTLogin
