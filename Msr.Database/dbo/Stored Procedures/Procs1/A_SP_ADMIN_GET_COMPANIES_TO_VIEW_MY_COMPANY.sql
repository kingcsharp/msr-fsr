
CREATE      PROCEDURE dbo.A_SP_ADMIN_GET_COMPANIES_TO_VIEW_MY_COMPANY
@strNTLogin nvarchar(50)
AS

declare @rootCo as varchar(50)
SELECT @rootCo = h.ROOT_COMPANY FROM A_PEOPLE a,A_PEOPLE_HISTORY h 
WHERE a.ID = @strNTLogin AND a.HISTORY_REF_ID = h.ID
print 'The Persons Root Company is ' + isNull(@rootCo,'NULL')

CREATE TABLE #tempExpandList(ID varchar(50))
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList values(@rootCo)
CREATE TABLE #tempCoTree(ID varchar(50),LEV int,HAS_CHILD smallInt,EXPANDED smallInt)

exec A_SP_COMPANY_ADD_TO_TREE_TABLE @rootCo,0,0

SELECT t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED,c.* 
FROM #tempCoTree t, A_V_COMPANIES_APPROVED_DATA c WHERE
c.ID = t.ID

--SELECT * FROM #tempCoTree

