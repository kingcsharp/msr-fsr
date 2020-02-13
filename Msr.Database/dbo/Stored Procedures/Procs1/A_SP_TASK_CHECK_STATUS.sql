
CREATE  PROCEDURE dbo.A_SP_TASK_CHECK_STATUS
@ID varchar(50),
@strNTLogin varchar(50),
@InvoiceForWO int
AS
if exists(SELECT * FROM A_TASKS WHERE ID = @ID AND SYSTEM_TASK = 'SYS_DNR')
	goto fin
if not exists(SELECT * FROM A_TASKS WHERE PARENT_ID = @ID AND STATUS NOT IN ('FINISHED','CLOSED'))
	begin
	print 'There are no children tasks open'
	IF (@InvoiceForWO = 1)
		BEGIN
			print 'We are invoicing for this WO. '
			UPDATE A_TASKS SET STATUS = 'FINISHED', ACTUAL_STOP_DATE = getDate()  WHERE ID = @ID
		END
	ELSE 
		BEGIN
			print 'We are NOT invoicing for this WO. '
			UPDATE A_TASKS SET STATUS = 'CLOSED', ACTUAL_STOP_DATE = getDate()  WHERE ID = @ID
		END
	
	exec A_SP_TASK_JUST_FINISHED_DO_EVERYTHING_NECESSARY @ID,@strNTlogin
	end
else
	begin
	print 'Should we set it to Accepted and Qued?'
	if not exists(SELECT * FROM A_TASKS WHERE PARENT_ID = @ID AND STATUS NOT IN ('FINISHED','CLOSED','Qued'))
		begin
		print 'There must be a qued child task'
		UPDATE A_TASKS SET STATUS = 'AccQued' WHERE ID = @ID
		end
	end

fin:

