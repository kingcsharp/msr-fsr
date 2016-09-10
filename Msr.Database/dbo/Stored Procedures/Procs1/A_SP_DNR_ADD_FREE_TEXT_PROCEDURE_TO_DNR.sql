CREATE procedure dbo.A_SP_DNR_ADD_FREE_TEXT_PROCEDURE_TO_DNR 
@DNR_ID varchar(50),
@PROC_ID varchar(50),
@TASK_TYPE varchar(50),
@strNTLogin varchar(50)
as
print 'Adding a freetext procedure to a DNR'
declare @procHistID varchar(50),@procObjID varchar(50)
SELECT @procHistID = OBJ_ID,@procObjID = ID FROM A_OBJECTS WHERE ID = @PROC_ID
print 'ProcHistID = ' + isnull(@procHistID,'NULL')

INSERT INTO A_DNR_PROC_INFO(ID,DNR_ID,PROC_ID,PROC_HIST_ID,PROC_OBJ_ID)
	VALUES(newID(),@DNR_ID,@PROC_ID,@procHistID,@procObjID)

UPDATE A_OBJECTS SET STATUS = 'APPROVED' WHERE ID = @procObjID
exec A_SP_PROCEDURES_FINISH_WF @procHistID,@PROC_ID,@strNTLogin

declare @newTaskID varchar(50),@msgs varchar(50)
exec dbo.A_SP_DNR_ADD_PROCEDURE_AS_CHILD_TASK @newTaskID OUTPUT,@msgs OUTPUT,
	@DNR_ID,@procHistID,@TASK_TYPE,@strNTLogin

print 'Task ID = ' + @newTaskID

UPDATE A_DNR_PROC_INFO SET TASK_ID = @newTaskID WHERE PROC_ID = @PROC_ID
UPDATE A_PROCEDURES SET PRIVATE_TO = @strNTLogin WHERE ID = @PROC_ID

print 'Task ID = ' + @newTaskID


print 'Editted the task info'

