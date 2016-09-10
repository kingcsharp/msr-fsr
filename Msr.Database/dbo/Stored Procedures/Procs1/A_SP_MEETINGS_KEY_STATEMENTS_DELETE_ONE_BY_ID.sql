

/*
STORED PROCEDURE CALLED IN meetings/editKeyStatments.asp
*/


CREATE   PROCEDURE A_SP_MEETINGS_KEY_STATEMENTS_DELETE_ONE_BY_ID
@keyStatementID varchar(50),
@strNTLogin varchar(50)
AS
print 'deleting Key Statement'

DELETE FROM A_MEETING_AGENDA_KEY_STATEMENTS 
WHERE ID = @keyStatementID
