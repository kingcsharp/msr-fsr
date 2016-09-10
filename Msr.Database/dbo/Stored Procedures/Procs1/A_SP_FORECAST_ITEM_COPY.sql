






CREATE       procedure A_SP_FORECAST_ITEM_COPY
	@newFID nvarchar(50) OUTPUT,
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one

INSERT INTO A_FORECAST_ITEMS
([ID], [FORECAST_ID], [ACCOUNT_ID], [F_TYPE], 
[QTY], [F_AMT], [PERCENT_OF_REV], [PROGRESS], 
[DRCM], [MODBY], [AVG_MONTHLY],
[CONFIDENCE], [NOTE], [AMT_INVOICED], [DATE_ADDED], 
[EST_QUAL_START_DATE], [ACT_QUAL_START_DATE], [EST_FIRST_PURCHASE_DATE], 
[ACT_FIRST_PURCHASE_DATE], [STATUS], [PRIORITY], [SUPPLIER_OWNER], 
[CUSTOMER_OWNER], [UNIT_PRICE], [UNIT_COST]
)
SELECT 
@newID, @newFID, [ACCOUNT_ID], [F_TYPE], 
[QTY], [F_AMT], [PERCENT_OF_REV], [PROGRESS], 
[DRCM], [MODBY], [AVG_MONTHLY],
[CONFIDENCE], [NOTE], [AMT_INVOICED], [DATE_ADDED],
[EST_QUAL_START_DATE], [ACT_QUAL_START_DATE], [EST_FIRST_PURCHASE_DATE], 
[ACT_FIRST_PURCHASE_DATE], [STATUS], [PRIORITY], [SUPPLIER_OWNER], 
[CUSTOMER_OWNER], [UNIT_PRICE], [UNIT_COST]





FROM A_FORECAST_ITEMS WHERE ID = @ID

print 'Copy all the ForeCast Distributions as well'
Declare @oneStep nvarchar(50)
Declare @stepCursor Cursor
set @stepCursor = Cursor
For SELECT ID FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @ID
open @stepCursor
Fetch Next from @stepCursor
Into @oneStep
while (@@fetch_status = 0)
	Begin
	print @oneStep
	exec A_SP_FORECAST_DISTRIBUTION_COPY @newID,@oneStep,@strNTLogin
	Fetch Next from @stepCursor
	Into @oneStep
	End
close @stepCursor
Deallocate @stepCursor				













