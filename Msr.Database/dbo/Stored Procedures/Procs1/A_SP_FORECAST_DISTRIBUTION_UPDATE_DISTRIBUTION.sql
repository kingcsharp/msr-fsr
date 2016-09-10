

CREATE PROCEDURE A_SP_FORECAST_DISTRIBUTION_UPDATE_DISTRIBUTION
@ID varchar(50),
@AMT_1 nvarchar(50),
@strNTLogin as varchar(50)
AS
declare @amt as money
set @amt = convert(money,isNull(@AMT_1,'0'))
UPDATE A_FORECAST_MONTHLY_BREAKDOWN SET AMT = @amt WHERE ID = @ID 


