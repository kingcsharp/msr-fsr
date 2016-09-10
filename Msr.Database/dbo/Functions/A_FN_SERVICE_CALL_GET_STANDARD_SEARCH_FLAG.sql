





CREATE         FUNCTION dbo.A_FN_SERVICE_CALL_GET_STANDARD_SEARCH_FLAG(@STATUS varchar(100))
RETURNS smallint
AS
BEGIN
if @STATUS = 'WORKER'
	return(1)
if @STATUS = 'BOSS'
	return(2)
if @STATUS = 'CUSTOMER_REVIEW_GROUP'
	return(3)
if @STATUS = 'CUSTOMER_AP_GROUP'
	return(4)
if @STATUS = 'AR_GROUP'
	return(5)
if @STATUS = 'CLOSED'
	return(6)
return(0)
END







