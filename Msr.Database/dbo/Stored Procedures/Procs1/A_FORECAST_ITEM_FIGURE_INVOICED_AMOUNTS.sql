


CREATE   PROCEDURE dbo.A_FORECAST_ITEM_FIGURE_INVOICED_AMOUNTS
@FIID varchar(50),
@strNTLogin varchar(50)
AS
declare @acctID varchaR(50),@forecastID varchar(50),@START_MONTH int,@START_YEAR int,@STOP_MONTH int,@STOP_YEAR int
SELECT @acctID = ACCOUNT_ID,@forecastID = FORECAST_ID FROM A_FORECAST_ITEMS WHERE ID = @FIID

SELECT @START_MONTH = START_MONTH,@START_YEAR = START_YEAR,
	@STOP_MONTH = STOP_MONTH,@STOP_YEAR = STOP_YEAR 
	FROM A_FORECASTS_HISTORY WHERE ID = @forecastID

print 'Acct ID = ' + isNull(@acctID,'NULL')

print 'SELECT * FROM A_ACCOUNT_INVOICE_ITEMS 
			WHERE ACCOUNT_ID = ''' + @acctID + ''' AND 
				(
					(month(DATE_POSTED) >= ' + convert(varchar(50),@START_MONTH) + ' 
					AND year(DATE_POSTED) >= ' + convert(varchar(50),@START_YEAR) + ')
					AND
					(month(DATE_POSTED) <= ' + convert(varchar(50),@STOP_MONTH) + ' 
					AND year(DATE_POSTED) <= ' + convert(varchar(50),@STOP_YEAR) + ')
				)
'


UPDATE A_FORECAST_ITEMS SET AMT_INVOICED = 
		isNull((SELECT SUM(AMOUNT) FROM A_ACCOUNT_INVOICE_ITEMS 
			WHERE ACCOUNT_ID = @acctID AND 
				((month(DATE_POSTED) >= @START_MONTH AND year(DATE_POSTED) >= @START_YEAR)
					AND
					(month(DATE_POSTED) <= @STOP_MONTH AND year(DATE_POSTED) <= @STOP_YEAR))
		),0)
WHERE ID = @FIID

Declare @mbID varchar(50),@MO int,@YR int
Declare @curs Cursor
set @curs = 
	Cursor For SELECT ID,MO,YR FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @FIID
open @curs
Fetch Next from @curs Into @mbID,@MO,@YR
while (@@fetch_status = 0)
	Begin
	UPDATE A_FORECAST_MONTHLY_BREAKDOWN SET AMT_INVOICED = 
		isNull((SELECT SUM(AMOUNT) FROM A_ACCOUNT_INVOICE_ITEMS 
			WHERE ACCOUNT_ID = @acctID AND 
				(
					(month(DATE_POSTED) = @MO AND year(DATE_POSTED) >= @YR)
				)

		),0)
			WHERE ID = @mbID
	Fetch Next from @curs Into @mbID,@MO,@YR
	End
close @curs
Deallocate @curs



