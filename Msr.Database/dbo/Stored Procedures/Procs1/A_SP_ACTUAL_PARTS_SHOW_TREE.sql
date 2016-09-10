CREATE  PROCEDURE DBO.A_SP_ACTUAL_PARTS_SHOW_TREE
@strRootPart varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempAPTree(ID varchar(50),LEV int,HAS_CHILD smallInt,EXPANDED smallInt)
exec A_SP_ACTUAL_PART_ADD_TO_TREE_TABLE @strRootPart,0,0

SELECT t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED,ap.* 
FROM #tempAPTree t, A_V_ACTUAL_PARTS_APPROVED_DATA ap WHERE
ap.ID = t.ID
