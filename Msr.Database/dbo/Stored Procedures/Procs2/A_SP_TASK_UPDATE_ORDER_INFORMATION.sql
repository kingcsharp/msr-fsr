

CREATE   PROCEDURE dbo.A_SP_TASK_UPDATE_ORDER_INFORMATION
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID nvarchar(50),
@TO_LOC  varchar(50),
@FROM_LOC  varchar(50),
@strNTLogin varchar(50)
AS
print 'Updating a Tasks Order Information'
print 'first find out if there is any information'
if not(exists(SELECT * FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @ID))
	begin
	print 'There is no info so we need to make some'
	INSERT INTO A_TASK_ORDER_INFORMATION (TASK_ID,MODBY,DRCM)
		VALUES(@ID,@strNTLogin,getDate())
	end
		
UPDATE A_TASK_ORDER_INFORMATION SET
	ACTUAL_TO_LOC = @TO_LOC,
	ACTUAL_FROM_LOC = @FROM_LOC
WHERE TASK_ID = @ID


