



CREATE   procedure A_SP_FORECAST_DISTRIBUTION_COPY
	@newFIID nvarchar(50) OUTPUT,
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one

INSERT INTO A_FORECAST_MONTHLY_BREAKDOWN
([ID], [MO], [YR], [AMT], [FI_ID], [DRCM], [MODBY])
SELECT 
@newID, [MO], [YR], [AMT], @newFIID, getDate(), @strNTLogin
FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE ID = @ID





