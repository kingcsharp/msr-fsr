CREATE PROCEDURE [dbo].[Portal_StepStartTask]
	@stepId varchar(50),
	@login varchar(50)
AS
	Declare @p1 varchar(4000)
	set @p1 = 'Error - The Status of this task does not allow acc'
	declare @p2 varchar(500)
	set @p2 = NULL
	exec A_SP_TASK_ACCEPT @p1 output, @p2 output, @stepId, @login
	Select @p1 AS RetStatus, @p2 AS Messages