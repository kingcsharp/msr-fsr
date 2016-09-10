


CREATE   FUNCTION timeToGrenich(@time datetime,@strNTLogin nvarchar(50))
RETURNS datetime
AS
BEGIN
	DECLARE @myOffset int
	SELECT @myOffset = isNull(p.G_DIFF,0) FROM A_APPROVED_PEOPLE p WHERE p.ID = @strNTLogin
	declare @newDate as dateTime
	set @newDate = DATEADD(hh,-(@myOffset),@time)
	return (@newDate)
END





