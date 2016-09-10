



CREATE    PROCEDURE A_SP_FORECAST_ITEM_FIX_DISTRIBUTION 
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Fixing the distribution'
declare @FID as varchar(50)
SELECT @FID = FORECAST_ID FROM A_FORECAST_ITEMS WHERE ID = @ID
declare @START_MO as int,@START_YR as int,@STOP_MO as int,@STOP_YR as int
SELECT @START_MO = START_MONTH,@START_YR=START_YEAR,@STOP_MO = STOP_MONTH,@STOP_YR=START_YEAR 
FROM A_FORECASTS_HISTORY WHERE ID = @FID
print 'The Start Month = ' + convert(varchar(50),@START_MO)
print 'The Stop Month = ' + convert(varchar(50),@STOP_MO)
print 'We need to delete any dist months that are out of range'
DELETE FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @ID AND 
(	YR < @START_YR OR YR > @STOP_YR OR
	(MO < @START_MO AND YR = @START_YR) OR (MO > @STOP_MO AND YR = @STOP_YR))
print 'Done Deleting them'
SELECT * FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @ID
declare @m as integer, @y as integer, @tester as varchar(50),@stop_m int
set @y = @START_YR

print 'Creating the months'
while @y <= @STOP_YR
	begin
	print 'if this is the first year then we start on the start month otherwise start with 1'
	if @y = @START_YR set @m = @START_MO
	else set @m = 1
	print 'If this is the last year then stop at the last month otherwise stop at 12'
	if @y = @STOP_YR set @stop_m = @STOP_MO
	else set @stop_m = 12
	while @m <= @stop_m
		begin
		set @tester = NULL
		SELECT @tester = ID FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE MO = @m AND YR = @Y AND FI_ID = @ID
		if @tester is null
			begin
				INSERT INTO A_FORECAST_MONTHLY_BREAKDOWN (ID,YR,MO,AMT,FI_ID,DRCM,MODBY) VALUES (newID(),@y,@m,0,@ID,getDate(),@strNTLogin)
			end
		set @m = @m + 1
		end
	set @y = @y + 1
	end

print 'If all the distributions = 0 then we need to evenly ditribute this value'
declare @maxDist as real
SELECT @maxDist = max(isNull(AMT,0)) FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @ID
if @maxDist = 0
	begin
	print 'The maximum Dist = 0'
	declare @evenAmt as money,@cnt as int,@tot as real
	SELECT @cnt = COUNT(ID) FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @ID
	print 'The count is '
	print @cnt
	SELECT @tot = F_AMT FROM A_FORECAST_ITEMS WHERE ID = @ID
	set @evenAmt = ROUND(@tot / @cnt,2)
	print 'The Even Amount is ' + convert(varchar(50),ROUND(@tot / @cnt,2))
	UPDATE A_FORECAST_MONTHLY_BREAKDOWN SET AMT = @evenAmt WHERE FI_ID = @ID
	end

print 'Now since we did round this thing and in case someone did some bad math we will distribute the remaining dollars over the first rows'
print 'First Figure out if the total and our distribution is equal.'
declare @sumTotal as money,@realTot as money,@diff as money
SELECT @sumTotal = SUM(isNull(AMT,0)) FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @ID
SELECT @realTot = F_AMT FROM A_FORECAST_ITEMS WHERE ID = @ID
declare @fb as varchar(50)
if @sumTotal <> @realTot
	begin
		print 'the @sumTotal and the real total are not equal'
		print 'The sum from the break down = ' + convert(nvarchar,@sumTotal)
		print 'The ORIG amount = ' + convert(nvarchar,@realTot)
		set @diff = @realTot - @sumTotal
		print 'the diff is'
		print convert(nvarchar,@diff)
		SELECT TOP 1 @fb = ID FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @ID ORDER BY YR,MO
		if @diff <> 0
			begin
			UPDATE A_FORECAST_MONTHLY_BREAKDOWN SET AMT = AMT + @diff WHERE ID = @fb
 			end
	end
	


SELECT * FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @ID







