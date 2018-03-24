

/*
STORED PROCEDURE CALLED IN serviceCalls/viewInvoiceServiceCalls.asp

*/

create     PROCEDURE A_SP_SERVICE_CALL_GET_INVOICE_DATA_BY_ID
@weeklyId varchar(50),
@strNTlogin varchar(50)
AS
SELECT * 
FROM A_V_SERVICE_CALL_INVOICE_DATA
WHERE ID = @weeklyID