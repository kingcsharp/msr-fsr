



/*
STORED PROCEDURE CALLED IN serviceCAlls/editServiceCalls.asp
*/
create    PROCEDURE A_SP_SERVICE_CALLS_GET_FILE_ATTACHMENTS 
@weeklyID nvarchar(50),
@strNTLogin nvarchar(50)
AS
SELECT * FROM A_V_SERVICE_CALLS_ATTACHMENT_DATA 
where ID = @weeklyID