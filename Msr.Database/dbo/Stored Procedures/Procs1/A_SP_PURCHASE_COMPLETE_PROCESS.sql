





CREATE       PROCEDURE dbo.A_SP_PURCHASE_COMPLETE_PROCESS
@retID varchar(50) OUTPUT,
@retMSG varchar(1000) OUTPUT,
@phID varchar(50),
--@MY_COMMENTS varchar(2000),
@strNTLogin varchar(50)
AS
declare @phObjID varchar(50)
SELECT @phObjID = OBJECT_ID FROM A_PURCHASES_HISTORY WHERE ID = @phID
UPDATE A_OBJECTS SET STATUS = 'APPROVED',LOCKED_BY = null,LOCKED_BY_NAME = NULL WHERE ID = @phObjID
exec A_SP_PURCHASES_FINISH_WF @phID,@phObjID,@strNTLogin
set @retID = @phID
set @retMSG = @phID

fin:





