CREATE      FUNCTION dbo.A_FN_SURVEYS_GET_STANDARD_SEARCH_FLAG(@STATUS varchar(50))
RETURNS smallint
AS
BEGIN
if @STATUS = 'OPEN'
	return(1)
if @STATUS = 'CREATING'
	return(2)
if @STATUS = 'CLOSED'
	return(3)
return(0)
END
