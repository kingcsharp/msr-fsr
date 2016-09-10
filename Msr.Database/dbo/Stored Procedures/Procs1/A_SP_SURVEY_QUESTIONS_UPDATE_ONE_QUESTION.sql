

/*
STORED PROCEDURE CALLED IN surveys/saveQuestionSurveys.asp
*/

CREATE                 PROCEDURE A_SP_SURVEY_QUESTIONS_UPDATE_ONE_QUESTION
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@questionID varchar(50),
@TEXT varchar(4000),
@NUM real,
@ROOT varchar(50),
@COLOR varchar(100),
@strNTLogin varchar(50)
AS
print 'questionID outside loop'
print @questionID
if @questionID is NULL
	begin
	   	print 'question id in loop' + isnull(@questionID,'null')
       	print 'Inserting a new Row' 
		exec SP_GETUNIQUEID3 @newID OUTPUT 
		INSERT INTO A_SURVEY_REPLIES ([ID],[ROOT],[AUTHOR], [MODBY],[DRCM])
		VALUES
		(@newID, @ROOT,@strNTLogin, @strNTLogin, getDate())
        	set @questionID=@newID
     end
	print 'updating item'
	UPDATE A_SURVEY_REPLIES 
		set TEXT = @TEXT,
		NUM = @NUM,
		COLOR = @COLOR,
		DRCM = getDate(),
		MODBY = @strNTLogin
	WHERE ID = @questionID
exec A_SP_SURVEY_ORDER_QUESTIONS @ROOT, @strNTLogin    

