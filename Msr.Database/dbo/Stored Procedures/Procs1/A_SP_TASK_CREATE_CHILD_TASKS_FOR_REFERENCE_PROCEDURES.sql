

/*
NOTE: This stored procedure is called in 

update accordingly. 
*/
CREATE   PROCEDURE DBO.A_SP_TASK_CREATE_CHILD_TASKS_FOR_REFERENCE_PROCEDURES
@PARENT_ID  varchar(50),
@strNTLogin varchar(50)
AS
print 'Creating a task from each of the reference procedures in the task A_SP_TASK_CREATE_CHILD_TASKS_FOR_REFERENCE_PROCEDURES'
print 'The parent Task is ' + @PARENT_ID
declare @procID varchar(50),@requestor varchar(50),@requesteeRole varchar(50),
	@requesteeID varchar(50),@procStepID varchar(50),@origSTDate datetime

SELECT @requestor = ORIG_REQUESTOR_ID,@procStepID = PROCEDURE_STEP_ID,
	@origSTDate = ORIG_PLANNED_START_DATE,@requesteeID = REQUESTEE_ID
	FROM A_TASKS WHERE ID = @PARENT_ID

Declare @curs Cursor,@it varchar(50),@doSteps tinyint
set @curs = Cursor For SELECT PROCEDURE_LINK AS ID FROM A_PROCEDURE_STEP_PROCEDURE_LINK 
					WHERE PROCEDURE_STEP = @procStepID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	if exists (SELECT * FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = @it AND STEPS_IN_AP = 1)
		begin
		print 'We have a reference procedure. so we should create a task for it and possibly its children.'
		exec A_SP_PROCEDURE_ASSIGN_TO_PEOPLE
			@it,@requesteeID,@requesteeRole,@origSTDate,@parent_ID,@strNTLogin
		end
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print ' We need to see if the parent procedure has an object we are working on.  If it does then we need to make all the children have it to'
INSERT INTO A_TASK_OBJECT_LINK (ID,TASK_ID,OBJECT_ID,DRCM,MODBY)
SELECT newID(),a.CHILD_ID,t.OBJECT_ID,getDate(),'sys' 
	FROM A_TASK_OBJECT_LINK t,
		A_TASKS_RELATION_TABLE a
	WHERE a.PARENT_ID = @PARENT_ID and a.PARENT_ID = t.TASK_ID and CHILD_ID <> @PARENT_ID
	and not exists (SELECT ID FROM A_TASK_OBJECT_LINK WHERE OBJECT_ID = t.OBJECT_ID and TASK_ID = a.CHILD_ID)	




fin:

print 'Finished with A_SP_TASK_CREATE_CHILD_TASKS_FOR_REFERENCE_PROCEDURES'

















