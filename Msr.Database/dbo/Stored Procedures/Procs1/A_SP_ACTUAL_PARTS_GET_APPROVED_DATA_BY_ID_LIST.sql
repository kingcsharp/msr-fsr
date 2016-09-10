
CREATE  PROCEDURE DBO.A_SP_ACTUAL_PARTS_GET_APPROVED_DATA_BY_ID_LIST
@strIDList varchar(50),
@strNTLogin varchar(50)
AS

CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @strIDList,','
SELECT *,ID + isNull('('+NICK_NAME+')','') + isNull('['+SERIAL+']','') + isNull('['+PART_DESC+']','') AS LONG_NAME FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID IN (SELECT LTRIM(IT) FROM #TempItems)
DROP TABLE #TempItems

