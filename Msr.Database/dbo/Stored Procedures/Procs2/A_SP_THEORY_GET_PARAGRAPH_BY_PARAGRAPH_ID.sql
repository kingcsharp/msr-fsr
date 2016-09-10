
/*
STORED PROCEDURE CALLED IN meeting/listAgendaMeeting.asp
*/
CREATE             PROCEDURE A_SP_THEORY_GET_PARAGRAPH_BY_PARAGRAPH_ID
@paragraphID nvarchar(50),
@strNTLogin nvarchar(50)
AS
SELECT * FROM A_THEORY_PARAGRAPHS 
WHERE ID=@paragraphID

