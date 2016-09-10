











CREATE             PROCEDURE dbo.A_SP_FORECASTS_VIEW_FORECASTS_ADD_MONTHLY_DATA_FOR_ITEM 
@ID varchar(50),
@firstMonth dateTime,
@lastMonth dateTime
AS
declare @vperc as float
declare @fType varchar(50)
declare @fID varchar(50)
print 'getting basic data from the Forecast Item Table with this query:'
print 'SELECT @vperc = PERCENT_OF_REV,F_TYPE FROM A_FORECAST_ITEMS WHERE ID = ''' + @ID + ''''
SELECT @vperc = PERCENT_OF_REV,@fType = F_TYPE,@fID = FORECAST_ID FROM A_FORECAST_ITEMS WHERE ID = @ID
declare @monthCounter dateTime,@sql varchar(8000)
set @monthCounter = @firstMonth
--'Sset the name and Paretn ID too
print 'Setting the name and parent ID of this item'
declare @acCom varchar(10),@acName varchar(2000),@conF float,@prog varchar(50),@pID varchar(50)
SELECT @acName = ACCOUNT_NAME + isNull(' ('+convert(varchar(50),PERCENT_OF_REV)+'%)',''),
		@conF = CONFIDENCE,
		@prog = PROGRESS,
		@pID = PARENT_FORECAST_ITEM,
		@acCom = left(NOTE,10)
		FROM A_V_FORECAST_ITEMS_WITH_ACCOUNT_PARENTS WHERE ID = @ID
UPDATE #tempForecast
	set NAME = @acName,
		CONFIDENCE = @conF,
		PROGRESS = @prog,
		PARENT_ID = @pID,
		SHORT_COMMENT = @acCom
	WHERE ID = @ID
	


set @sql = 'UPDATE #tempForecast SET '
while @monthCounter <= @lastMonth
	begin



	print 'Adding Data for month = ' + convert(varchar(50),month(@monthCounter)) + '/' + convert(varchar(50),year(@monthCounter))
	if @fType = 'VAR_COS'
		begin
		print 'This is a Variable cost item so do the special thing for that'
		set @sql = @sql + ' MONTH_' + 
			convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
			' = ' + convert(varchar(50),(@vperc/100)) + ' * isNull((SELECT MONTH_' + 
			convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
			' FROM #tempForecast WHERE ID = ''' + @fid + '_TSR'' AND FORECAST_ID = ''' + @fID + '''),0),'

		set @sql = @sql + ' ACT_MONTH_' + 
			convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
			' = ' + ' isNull((SELECT AMT_INVOICED FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = ''' + @ID + 
					''' AND MO = ''' + convert(varchar(50),month(@monthCounter)) + 
					''' AND YR = ''' + convert(varchar(50),year(@monthCounter)) + '''' + '),0), '

		print 'TESTING THIS RIGHT HERE' + @sql
		end
	else
		begin
		if @fType = 'TAX_F'
			begin
			print 'This is a Tax item so do the special thing for that'
			set @sql = @sql + ' MONTH_' + 
				convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
				' = ' + convert(varchar(50),isNull(@vperc,0)) + ' * ((isNull((SELECT MONTH_' + 
				convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
				' FROM #tempForecast WHERE ID = ''' + @fid + '_EBT'' AND FORECAST_ID = ''' + @fID + '''),0))/100),'
			set @sql = @sql + ' ACT_MONTH_' + 
				convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
				' = ' + convert(varchar(50),isNull(@vperc,0)) + ' * ((isNull((SELECT ACT_MONTH_' + 
				convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
				' FROM #tempForecast WHERE ID = ''' + @fid + '_EBT'' AND FORECAST_ID = ''' + @fID + '''),0))/100),'

			end
		else
			begin
			print 'This is a normal item so do the special thing for that'
			set @sql = @sql + ' MONTH_' + 
				convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
				' = ' + ' isNull((SELECT AMT FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = ''' + @ID + 
				''' AND MO = ''' + convert(varchar(50),month(@monthCounter)) + 
				''' AND YR = ''' + convert(varchar(50),year(@monthCounter)) + '''' + '),0),'
			print 'Adding the actuals for each item here'
			set @sql = @sql + ' ACT_MONTH_' + 
				convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
				' = ' + ' isNull((SELECT AMT_INVOICED FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = ''' + @ID + 
						''' AND MO = ''' + convert(varchar(50),month(@monthCounter)) + 
						''' AND YR = ''' + convert(varchar(50),year(@monthCounter)) + '''' + '),0), '
			print @sql
			end
		end
	set @monthCounter = dateAdd(mm,1,@monthCounter)	
	end
set @sql = left(@sql,len(@sql)-1)
set @sql = @sql + ' WHERE ID = ''' + @ID + ''' '
print isNull(@sql,'This is NULL')
exec(@sql)













