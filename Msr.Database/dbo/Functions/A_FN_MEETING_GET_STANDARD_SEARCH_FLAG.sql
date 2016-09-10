

CREATE     FUNCTION dbo.A_FN_MEETING_GET_STANDARD_SEARCH_FLAG(@START_DATE datetime,@END_DATE datetime,@curDate dateTime)
RETURNS smallint
AS
BEGIN
if @START_DATE < @curDate AND @END_DATE > @curDate
	return(1)
if @START_DATE > @curDate
	return(2)
if @START_DATE < @curDate
	return(3)
return(0)
END



