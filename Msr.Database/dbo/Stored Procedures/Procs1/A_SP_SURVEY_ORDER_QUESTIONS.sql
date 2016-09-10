










/*
STORED PROCEDURE CALLED IN meeting/listAgendaMeeting.asp
*/
CREATE              PROCEDURE A_SP_SURVEY_ORDER_QUESTIONS
@surveyID varchar (50),
@strNTLogIn varchar(50)
AS
 

Declare @curs Cursor
Declare @it varchar(50)
Declare @counter int  
set @counter =0

PRINT 'REORDERING SURVEYS'

set @curs = Cursor For 
	SELECT  ID FROM A_SURVEY_REPLIES 
	WHERE ROOT=@surveyID ORDER BY NUM
	open @curs
	Fetch Next from @curs Into @it
		while (@@fetch_status = 0)
		   Begin
			set @counter = @counter +1 
	        	UPDATE A_SURVEY_REPLIES 
			   set NUM = @counter
		       	WHERE ID = @it 
		 	Fetch Next from @curs Into @it
	            End

	close @curs
Deallocate @curs


























