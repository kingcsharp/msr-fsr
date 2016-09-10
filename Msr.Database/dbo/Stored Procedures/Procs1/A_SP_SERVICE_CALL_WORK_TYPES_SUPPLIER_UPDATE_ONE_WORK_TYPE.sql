




CREATE    PROCEDURE dbo.A_SP_SERVICE_CALL_WORK_TYPES_SUPPLIER_UPDATE_ONE_WORK_TYPE
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID nvarchar(50),
@SKIP_BOSS varchar(50),
@strNTLogin varchar(50)
AS
declare @supplierID varchar(50)
SELECT @supplierID = SUPPLIER_ID FROM A_SERVICE_CALLS_WORK_TYPES WHERE ID = @ID
declare @myCo varchar(50)
SELECT @myCo = COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
if @myCo = @supplierID
	begin
	UPDATE A_SERVICE_CALLS_WORK_TYPES SET SKIP_BOSS = convert(tinyInt,@SKIP_BOSS) WHERE ID = @ID
	end
else
	begin
	set @messages = 'Error You are not in the supplier co'
	print 'Not the supplier error'
	end

