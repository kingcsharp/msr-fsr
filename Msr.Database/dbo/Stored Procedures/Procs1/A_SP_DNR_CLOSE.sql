

CREATE   PROCEDURE dbo.A_SP_DNR_CLOSE
@newID varchar(50) OUTPUT,
@msgs varchar(1000) OUTPUT,
@ID varchar(50),
@strNTLogin varchar(50)
AS
exec A_SP_TASK_FINISH @ID,@strNTLogin
declare @actPartObjID varchar(50),@phID varchar(50),@fillID varchar(50)
SELECT @phID = PURCHASE_HIST_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @ID
print 'phID = ' + isNull(@phID,'NULL')
SELECT @fillID = FILL_ID FROM A_TASK_ORDER_INFORMATION 
	WHERE  PURCHASE_HIST_ID = @phID AND FILL_ID IS not  NULL
print 'fillID = ' + isNull(@fillID,'NULL')
SELECT @actPartObjID = FILL_OBJ_ID FROM A_V_FILLS_SEARCH WHERE ID = @fillID

UPDATE A_ACTUAL_PARTS_HISTORY SET AP_STATUS = 'ap_available' WHERE OBJECT_ID = @actPartObjID






