
CREATE           PROCEDURE dbo.A_SP_PURCHASE_SHOW_ITEM_TREE
@strPurchaseID varchar(50),
@strQuoteID varchar(50),
@strShowAll varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@showAddCost tinyInt,
@strNTLogin nvarchar(50)
AS

CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempItemTree(srt int IDENTITY,ID varchar(50),LEV int,HAS_CHILD smallInt,EXPANDED smallInt)

declare @curs as cursor,@it varchar(50)
set @curs = CURSOR for SELECT ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = @strPurchaseID and PARENT is NULL 
--and QUOTE_ID = @strQuoteID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	if @strShowAll = 'true' INSERT INTO #tempExpandAllList Values(@it)
	print 'Getting the first Item Number = ' + @it
	exec A_SP_ORDER_SHOW_ITEM_TREE_ADD_ONE_TO_TREE @it,0,0,@showAddCost
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

SELECT t.srt as SRT,t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED,o.* 
FROM #tempItemTree t, A_V_ORDER_ITEMS_DATA_WITH_SHIPPING o WHERE
o.ID = t.ID ORDER BY t.srt


-- SELECT t.ID, t.srt as SRT,t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED
--	FROM #tempItemTree t




--SELECT * FROM #tempCoTree

fin:
return 0 

problem:
print 'Error order ID = ' + isnull(@strPurchaseID,'NULL')
return 1 


















