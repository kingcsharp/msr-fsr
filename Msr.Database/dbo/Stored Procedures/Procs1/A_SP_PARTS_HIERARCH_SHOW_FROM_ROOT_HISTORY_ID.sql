

CREATE    PROCEDURE DBO.A_SP_PARTS_HIERARCH_SHOW_FROM_ROOT_HISTORY_ID
@strAPartHistID varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempAPTree(ID varchar(50),LEV int,HAS_CHILD smallInt,EXPANDED smallInt)
exec A_SP_PART_HIERARCHY_ADD_ITEM_TO_TREE_TABLE @strAPartHistID,0,0
SELECT t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED,p.* 
FROM #tempAPTree t, A_O_PARTS_HISTORY p WHERE
p.ID = t.ID ORDER BY t.LEV,p.NAME


