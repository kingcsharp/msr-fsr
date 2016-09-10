

CREATE   PROCEDURE [dbo].[A_SP_FILL_CANCEL_UNFINISHED_STEPS]
@RET_STATUS as varchar(500) OUTPUT,
@MSGS as varchar(50) OUTPUT,
@FILL_ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Deleting all unfinished steps for fill = ' + @FILL_ID
declare @parentTask varchar(50)
SELECT @parentTask = TASK_ID FROM A_TASK_ORDER_INFORMATION with (noLock) WHERE FILL_ITEM_ID =  @FILL_ID
print ' Got the parentTask = ' + @parentTask
exec A_SP_TASK_CANCEL_UNFINISHED_CHILD_STEPS @RET_STATUS OUTPUT,@MSGS OUTPUT, @parentTask, @strNTLogin
exec A_SP_TASK_FIGURE_OUT_CHILD_STATUS @parentTask, @strNTLogin
exec A_SP_TASK_CHECK_STATUS @parentTask, @strNTLogin
