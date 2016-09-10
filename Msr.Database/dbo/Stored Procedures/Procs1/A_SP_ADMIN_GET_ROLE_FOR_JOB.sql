






CREATE  PROCEDURE dbo.A_SP_ADMIN_GET_ROLE_FOR_JOB
@JOB varchar(50),
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

CREATE TABLE #tempJobList (CO_ID varchar(50),ROLE_NAME nvarchar(1000),ROLE_ID varchar(50),
			CO_NAME nvarchar(1000),JOB varchar(50))

INSERT INTO #tempJobList (CO_ID,ROLE_NAME,ROLE_ID,CO_NAME,JOB)
	SELECT CO_ID,ROLE_NAME,ROLE_ID,CO_NAME,JOB FROM A_V_ADMIN_ROLE_JOBS_WITH_CO_AND_ROLE
		WHERE JOB = @JOB

SELECT t.ID, t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED,c.*,com.NAME AS COMPANY_NAME
FROM #tempCoTree t 
	LEFT OUTER JOIN #tempJobList c ON c.CO_ID = t.ID
	INNER JOIN A_V_COMPANIES_APPROVED_DATA com on t.ID = com.ID












