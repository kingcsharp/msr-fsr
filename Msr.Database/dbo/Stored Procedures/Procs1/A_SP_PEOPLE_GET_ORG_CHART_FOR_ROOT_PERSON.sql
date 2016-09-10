



CREATE    PROCEDURE DBO.A_SP_PEOPLE_GET_ORG_CHART_FOR_ROOT_PERSON
@strRootCo varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempCoTree(ID varchar(50),LEV int,HAS_CHILD smallint,EXPANDED smallInt,idt smallint identity)
exec A_SP_PEOPLE_ADD_TO_TREE_TABLE @strRootCo,0,0
SELECT t.LEV AS TREE_LEVEL,t.EXPANDED AS EXPANDED,t.HAS_CHILD AS TREE_HAS_CHILD,
p.*,r.NAME AS CO_POSITION_NAME,p.ID as ROOT FROM 
#tempCoTree t
INNER JOIN A_V_PEOPLE_APPROVED_DATA p ON p.ID = t.ID
LEFT OUTER JOIN A_V_ROLES_APPROVED_DATA r ON r.ID = p.CO_POSITION
ORDER BY t.idt






