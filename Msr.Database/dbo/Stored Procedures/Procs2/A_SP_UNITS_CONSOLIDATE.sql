
/*
This procedure REquires a temp table be made before calling it called
CREATE TABLE #tempUnitRollUpTable (UNIT varchar(50),QTY float)
*/

CREATE PROCEDURE DBO.A_SP_UNITS_CONSOLIDATE
@qty float OUTPUT,
@unitID varchar(50) OUTPUT
AS
print 'Consolidating Units'
SELECT 'Data Table',* FROM #tempUnitRollUpTable
print 'first get the total using base view'
SELECT 'Data Table w Base',s.QTY,t.STANDARD_UNIT 
	FROM #tempUnitRollUpTable s, A_Z_UNITS_BASE_UNIT_CONVERTER t 
	WHERE s.UNIT = t.FROM_UNIT
declare @totQty as float
SELECT @totQty = sum(s.QTY * t.STANDARD_UNIT) 
	FROM #tempUnitRollUpTable s, A_Z_UNITS_BASE_UNIT_CONVERTER t 
	WHERE s.UNIT = t.FROM_UNIT
print '@totQty = ' + convert(varchar(50),@totQty)

/*declare @maxUnit float
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

*/

