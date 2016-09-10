



/*
STORED PROCEDURE CALLED IN meetings/editKeyStatments.asp
*/

CREATE       PROCEDURE A_SP_MEETING_AGENDA_KEY_STATEMENT_UPDATE_ONE_STATEMENT
@keyStatementID varchar(50),
@agendaID varchar(100),
@key_statment_text varchar(4000),
@num real,
@strNTLogin varchar(50)
AS
print 'Updating Key Statement'

if @keyStatementID ='NEWONE' 
begin
    
    declare @newID varchar(50)
	exec SP_GETUNIQUEID3 @newID OUTPUT 
	INSERT INTO A_MEETING_AGENDA_KEY_STATEMENTS ([ID],AGENDA_ID)
		VALUES(@newID, @agendaID)
       set @keyStatementID = @newID
end
 	UPDATE A_MEETING_AGENDA_KEY_STATEMENTS 
	SET KEY_STATEMENTS = @key_statment_text,
   	NUM = @num,
	DRCM = getDate(),
	MODBY = @strNTLogin
	WHERE ID = @keyStatementID



