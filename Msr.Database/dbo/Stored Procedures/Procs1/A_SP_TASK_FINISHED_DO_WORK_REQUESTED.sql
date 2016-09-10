

CREATE   PROCEDURE dbo.A_SP_TASK_FINISHED_DO_WORK_REQUESTED
@ID varchar(50),
@strNTLogin varchar(50)
AS
print ' In A_SP_TASK_FINISHED_DO_WORK_REQUESTED '
print 'Making sure that everything is done that is supposed to be when a task is finished'
--exec A_SP_TASK_FINISH_PROCEDURE @ID,@strNTLogin

exec A_SP_TASK_START_FOLLOWING_STEPS @ID,@strNTLogin



