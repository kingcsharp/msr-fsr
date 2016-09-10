


CREATE      FUNCTION dbo.A_FN_NEEDS_GET_STANDARD_SEARCH_FLAG(@START_DATE datetime,@STOP_DATE datetime,@curDate dateTime)
RETURNS smallint
AS
BEGIN
if @STOP_DATE < @curDate
	return(2)
if @START_DATE > @curDate
	return(1)
return(0)
END




