






/*
STORED PROCEDURE CALLED IN serviceCall/editServiceCall.asp

*/
CREATE       PROCEDURE A_SP_SERVICE_CALL_GET_HOURS_WORKED
@weeklyID varchar(50),
@hourType varchar(50),
@strNTLogin nvarchar(50)
AS
--SELECT CASE WHEN (HOURS = 0) THEN  ''
--ELSE convert(varchar(50),HOURS)
--end HOURS		
--FROM A_SERVICE_CALL_WORK_TIME
--WHERE WEEKLY_ID =@weeklyID AND HOUR_TYPE =@hourType
--ORDER BY YR,MO,D

SELECT *
FROM A_SERVICE_CALL_WORK_TIME
WHERE WEEKLY_ID =@weeklyID AND HOUR_TYPE =@hourType
ORDER BY YR,MO,D








