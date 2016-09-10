



/*
STORED PROCEDURE CALLED IN meetings/editAgendaMeeting.asp
*/

CREATE   PROCEDURE A_SP_SURVEY_GET_QUESTION_COUNT 
@lastItem int OUTPUT,
@msg varchar(1000) OUTPUT,
@surveyID varchar(50) OUTPUT,
@strNTLogin varchar(50)
AS

print 'Getting number of last item'
SELECT @lastItem = COUNT(NUM) 
FROM A_SURVEY_REPLIES 
WHERE ROOT=@surveyID

set @lastItem = @lastItem +1 
print 'count is'
print +@lastItem














































