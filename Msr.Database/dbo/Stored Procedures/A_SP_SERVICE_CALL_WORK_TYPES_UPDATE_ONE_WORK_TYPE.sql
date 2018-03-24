


/*
STORED PROCEDURE CALLED IN worktypes/editWorkTypes.asp
*/
CREATE       PROCEDURE A_SP_SERVICE_CALL_WORK_TYPES_UPDATE_ONE_WORK_TYPE
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@workTypeID varchar(50),
@supplierID varchar(50),
@customerID varchar(50),
@workTypeName varchar(1000),
@approverRole varchar(50),
@payerRole varchar(50),
@hourRate varchar(50),
@otRate varchar(50),
@taxRate real,
@hide tinyint,
@strNTLogin varchar(50)
AS
declare @normalRate money
declare @overRate money

set @normalRate = convert(money, @hourRate,2)
set @overRate = convert(money, @otRate,2)


print 'inside update workType'
if @workTypeID is null
begin
 	print 'we are making a New worktype'
 	exec SP_GETUNIQUEID3 @newID OUTPUT 
	INSERT INTO A_SERVICE_CALLS_WORK_TYPES 
	([ID])
	VALUES(@newID)
	set @worktypeID=@newID
	print 'Created a new worktype witht the id of ' + @worktypeID
end
print 'we are updating the worktype with worktype id of ' + @worktypeID
UPDATE A_SERVICE_CALLS_WORK_TYPES 
set SUPPLIER_ID=@supplierID,
CUSTOMER_ID = @customerID ,
WORK_TYPE_NAME = @workTypeName,
APPROVER_ROLE = @approverRole,
PAYER_ROLE = @payerRole,
HOUR_RATE = @normalRate,
OT_RATE = @overRate,
TAX_RATE = @taxRate,
DRCM = getDate(),
MODBY = @strNTLogin,
HIDE = @hide
WHERE ID = @worktypeID