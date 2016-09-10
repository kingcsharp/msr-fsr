






CREATE      PROCEDURE dbo.A_SP_FORECAST_ITEM_SHOW_INVOICED_ITEMS
@strWhere nvarchar(4000),
@strSort nvarchar(1000),
@fiID varchar(50),
@strNTLogin nvarchar(50)
AS

declare @acctID varchaR(50),@forecastID varchar(50),@START_MONTH int,@START_YEAR int,@STOP_MONTH int,@STOP_YEAR int
SELECT @acctID = ACCOUNT_ID,@forecastID = FORECAST_ID FROM A_FORECAST_ITEMS WHERE ID = @FIID

SELECT @START_MONTH = START_MONTH,@START_YEAR = START_YEAR,
	@STOP_MONTH = STOP_MONTH,@STOP_YEAR = STOP_YEAR 
	FROM A_FORECASTS_HISTORY WHERE ID = @forecastID



declare @sql nvarchar(4000)
set @sql = 'SELECT * FROM A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA 
			WHERE ACCOUNT_ID = ''' + @acctID + ''' AND
				((month(DATE_POSTED) >= ' + convert(varchar(50),@START_MONTH) + ' AND year(DATE_POSTED) >= ' + convert(varchar(50),@STOP_YEAR) + ')
					AND
					(month(DATE_POSTED) <= ' + convert(varchar(50),@START_MONTH) + ' AND year(DATE_POSTED) <= ' + convert(varchar(50),@STOP_YEAR) + '))
		'

if len(@strWhere) > 0
		set @sql = @sql + ' AND (' + @strWhere + ') '

if len(@strSort) > 0
		set @sql = @sql + @strSort

exec(@sql)







