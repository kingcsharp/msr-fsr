


CREATE    PROCEDURE DBO.A_SP_Z_DROP_DOWN_LIST_POPULATOR
@strDDLists as varchar(8000),
@strNTLogin as varchar(50)
AS

CREATE TABLE #TempItems	(IT varchar(200))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @strDDLists,','
print 'get here'
SELECT * FROM A_Z_DROP_DOWN_POPULATOR WHERE USER_ID = @strNTLogin AND POP_UP_KEY in 
(SELECT IT FROM #TempItems) AND SHOW IS NOT NULL AND SHOW <> ''
ORDER BY POP_UP_KEY,SHOW
print 'Done '


