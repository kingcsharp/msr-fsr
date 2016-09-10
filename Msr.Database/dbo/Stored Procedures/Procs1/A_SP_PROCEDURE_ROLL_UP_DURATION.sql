

CREATE   PROCEDURE DBO.A_SP_PROCEDURE_ROLL_UP_DURATION
@ID varchar(50)
AS
declare @totSecs as float
SELECT @totSecs = sum(s.DURATION * t.SECS)
	FROM dbo.A_PROCEDURE_STEPS s INNER JOIN
         dbo.A_Z_UNITS_TIME_TO_SECS t ON s.DURATION_TYPE = t.FROM_UNIT
	WHERE s.PROCEDURE_ID = @ID
print '@totSecs = ' + convert(varchar(50),@totSecs)
declare @maxUnit float
SELECT @maxUnit = max(t.SECS) 
	FROM dbo.A_PROCEDURE_STEPS s INNER JOIN
         dbo.A_Z_UNITS_TIME_TO_SECS t ON s.DURATION_TYPE = t.FROM_UNIT
	WHERE s.PROCEDURE_ID = @ID
print 'max unit = ' + convert(varchar(50),@maxUnit)
declare @maxUnitID varchar(50)
SELECT @maxUnitID = FROM_UNIT FROM A_Z_UNITS_TIME_TO_SECS WHERE SECS = @maxUnit
print 'id = ' + @maxUnitID
declare @totOfMaxUnit float
set @totOfMaxUnit = @totSecs / @maxUnit
print 'tot of Unit = ' + convert(varchar(50),@totOfMaxUnit)
declare @currentTime float
SELECT @currentTime = (p.DURATION * T.SECS) 
	FROM A_PROCEDURES_HISTORY p INNER JOIN A_Z_UNITS_TIME_TO_SECS t
		ON p.DURATION_TYPE = t.FROM_UNIT
		WHERE p.ID = @ID
print 'procTime = ' + isNull(convert(varchar(50),@currentTime),'NULL')
if @totOfMaxUnit is not null
--if (@currentTime is null) or  (@currentTime < @totSecs)
	begin
	print 'Need to adjust the time'
	UPDATE A_PROCEDURES_HISTORY SET 
		DURATION = @totOfMaxUnit, 
		DURATION_TYPE = @maxUnitID
	WHERE ID  = @ID
	end


