

/*
STORED PROCEDURE CALLED IN serviceCalls/editServiceCalls.asp

*/
create     PROCEDURE A_SP_SERVICE_CALL_GET_DATA_BY_ID
@weeklyId varchar(50),
@strNTlogin varchar(50)
AS
SELECT * 
FROM A_V_SERVICE_CALL_WEEKLY_REPORT_DATA
WHERE ID = @weeklyID
