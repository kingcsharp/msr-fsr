





CREATE       PROCEDURE dbo.A_SP_PART_SAFETY_STOCK_LEVEL_UPDATE_FOR_LOCATION_AND_PART
@lID varchar(50),
@pID varchar(50)
AS
declare @myQty float
SELECT @myQty = sum(QTY) FROM A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK WHERE
	PART_ID = @pID AND LOCATION = @lID AND AP_STATUS in ('ap_available','ap_in_fill','ap_in_call')
print '^^^^^^^^^^^^^^IN HERE^^^^^^^^^^^^^^^^'
UPDATE A_PARTS_SAFETY_STOCK_LEVELS SET OLD_LEVEL = CUR_LEVEL,CUR_LEVEL = @myQty WHERE
	PART_ID = @pID AND LOCATION_ID = @lID AND STATUS LIKE 'APPROVED%' AND CUR_LEVEL <> @myQty
print '%%%%%%%%%%%%%Done Updating%%%%%%%%%%%%'






