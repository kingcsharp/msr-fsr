




CREATE     PROCEDURE A_SP_THEORY_GET_PARAGRAPH_COUNT 
@lastItem int OUTPUT,
@msg varchar(1000) OUTPUT,
@TOID varchar(50),
@strNTLogin varchar(50)
AS
declare @strID as nvarchar(50)
SELECT @strID = ID FROM A_THEORY_HISTORY WHERE OBJECT_ID = @TOID

print 'Getting number of last item'
SELECT @lastItem = COUNT(PARAGRAPH_ORDER_NUMBER) FROM A_THEORY_PARAGRAPHS WHERE THEORY_ID=@strID

set @lastItem = @lastItem +1 
print 'count is'
print +@lastItem





