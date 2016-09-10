




CREATE   PROCEDURE dbo.A_SP_COMPANIES_SHOW_ALL_COMPANIES_IN_A_TREE
@strNTLogin nvarchar(50)
AS

declare @rootCo as varchar(50)
SELECT @rootCo = h.ROOT_COMPANY FROM A_PEOPLE a,A_PEOPLE_HISTORY h 
WHERE a.ID = @strNTLogin AND a.HISTORY_REF_ID = h.ID
print 'The Persons Root Company is ' + isNull(@rootCo,'NULL')

CREATE TABLE #tempExpandList(ID varchar(50))
--INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
--INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempCoTree(ID varchar(50),LEV int,HAS_CHILD smallInt,EXPANDED smallInt)

declare @curs as CURSOR
declare @it as varchar(50)
SET @curs = CURSOR for SELECT ID FROM A_V_COMPANIES_APPROVED_DATA WHERE PARENT is NULL AND CO_TYPE = 'COMPANY'
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Root Company = ' + @it
	exec A_SP_COMPANY_ADD_TO_TREE_TABLE @it,0,1
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


SELECT t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED,c.* 
FROM #tempCoTree t, A_V_COMPANIES_APPROVED_DATA c WHERE
c.ID = t.ID 
AND (c.CO_TYPE = 'COMPANY' OR c.ROOT_CO = @rootCo)









