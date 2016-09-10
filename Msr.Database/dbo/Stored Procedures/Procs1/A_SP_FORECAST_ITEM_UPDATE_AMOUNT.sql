

CREATE PROCEDURE A_SP_FORECAST_ITEM_UPDATE_AMOUNT
@ID varchar(50),
@AMT_1 nvarchar(50),
@strNTLogin as varchar(50)
AS
declare @amt as money
set @amt = convert(money,isNull(@AMT_1,'0'))
UPDATE A_FORECAST_ITEMS SET F_AMT = @amt WHERE ID = @ID 


