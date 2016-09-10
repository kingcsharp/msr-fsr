
CREATE FUNCTION dbo.A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON
	(@strNTLogin varchar(50),@tm datetime)
RETURNS datetime
as
BEGIN
declare @myTZ varchar(50),@gDiff float,@myTime dateTime
SELECT @myTZ = TIME_ZONE FROM A_V_PEOPLE_APPROVED_DATA
	WHERE ID = @strNTLogin
SELECT @myTime = DATEADD(hh,G_DIFF,@tm) FROM A_TIME_ZONES WHERE ID = @myTZ
return(@myTime)
END

