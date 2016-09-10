




CREATE  PROCEDURE DBO.A_SP_LOCATION_GET_TREE
@strRootLocID varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempCoTree(ID varchar(50),LEV int,HAS_CHILD smallint,EXPANDED smallInt,idt smallint identity)
exec A_SP_LOCATION_ADD_TO_TREE_TABLE @strRootLocID,0,0
SELECT t.LEV AS TREE_LEVEL,t.EXPANDED AS EXPANDED,t.HAS_CHILD AS TREE_HAS_CHILD,
l.*,l.ID as ROOT FROM 
#tempCoTree t
INNER JOIN A_V_LOCATIONS_APPROVED_DATA l ON l.ID = t.ID
ORDER BY t.idt







