

CREATE PROCEDURE A_SP_FORECAST_ITEM_DELETE_ITEM
@ID varchar(50),
@strNTLogin as varchar(50)
AS
declare @FID as varchar(50)
SELECT @FID = FORECAST_ID FROM A_FORECAST_ITEMS WHERE ID = @ID
declare @objID as varchar(50)
SELECT @objID = OBJECT_ID FROM A_FORECASTS_HISTORY WHERE ID = @FID
declare @lockedBy as varchar(50)
SELECT @lockedBY = LOCKED_BY FROM A_OBJECTS WHERE ID = @objID
if @lockedBY = @strNTLogin
	begin
	print 'Deleting'
	DELETE FROM A_FORECAST_ITEMS WHERE ID = @ID
	end





