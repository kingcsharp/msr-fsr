

CREATE  PROCEDURE dbo.A_SP_PURCHASE_SHOW_QUOTE_ITEM_TREE
@strPurchaseID varchar(50),
@strQuoteID varchar(50),
@strShowAll varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
SELECT @strQuoteID = HISTORY_REF_ID FROM A_QUOTES WHERE ID = @strQuoteID
print 'The Quote ID is ' + isnull(@strQuoteID,'NULL')
if @strQuoteID is null goto problem

CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempItemTree(srt int IDENTITY,ID varchar(50),LEV int,HAS_CHILD smallInt,EXPANDED smallInt)

print 'SELECT ID FROM A_QUOTE_ITEMS WHERE QUOTE_ID = ''' + @strQuoteID + ''' and PARENT is NULL'

declare @curs as cursor,@it varchar(50)
set @curs = CURSOR for SELECT ID FROM A_QUOTE_ITEMS WHERE QUOTE_ID = @strQuoteID and PARENT is NULL
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	if @strShowAll = 'true' INSERT INTO #tempExpandAllList Values(@it)
	print 'Getting the first Order Item Number = ' + @it
	exec A_SP_QUOTE_SHOW_ITEM_TREE_ADD_ONE_TO_TREE @it,0,0
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

--SELECT * FROM #tempItemTree

SELECT t.ID AS ID,t.srt as SRT,t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED,o.* 
FROM #tempItemTree t, A_V_PURCHASES_WITH_QUOTE_ITEMS o 
WHERE
o.PURCHASE_ID = @strPurchaseID AND 
o.QUOTE_ITEM_ID = t.ID
ORDER BY t.srt

--SELECT * FROM #tempCoTree

fin:
return 0 

problem:
print 'Error order ID = ' + isnull(@strQuoteID,'NULL')
return 1 










