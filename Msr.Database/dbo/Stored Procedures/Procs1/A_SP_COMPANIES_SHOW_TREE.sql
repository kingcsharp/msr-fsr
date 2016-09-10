



CREATE        PROCEDURE A_SP_COMPANIES_SHOW_TREE
@strRootCo varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempCoTree(ID varchar(50),LEV int,HAS_CHILD smallInt,EXPANDED smallInt)
exec A_SP_COMPANY_ADD_TO_TREE_TABLE @strRootCo,0,0

SELECT t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED,c.* 
FROM #tempCoTree t, A_V_COMPANIES_APPROVED_DATA c WHERE
c.ID = t.ID

--SELECT * FROM #tempCoTree







