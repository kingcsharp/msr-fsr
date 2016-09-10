






CREATE            PROCEDURE DBO.A_SP_PURCHASES_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print @myID + '---'
declare @myRoot as nvarchar(50)
if @myID is null
	SELECT @myID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
if @objID is null
	SELECT @objID = ID FROM A_OBJECTS WHERE OBJ_ID = @myID

SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @objID

if @myRoot is null
	print 'My ROOT IS NULL'

if not exists (SELECT ID FROM A_PURCHASES WHERE ID = @myRoot)
	INSERT INTO A_PURCHASES (ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@myID,getDate(),@strNTLogin)

declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')

--This sets the History_REF_ID to be the correct one
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_PURCHASES','HISTORY_REF_ID'

UPDATE A_PURCHASES_HISTORY SET PURCHASE_STATUS = 'WAITING_FILLS' WHERE ID = @curID
exec A_SP_FILLS_SET_UP_FILL_FOR_PURCHASE @myID,@strNTLogin

exec A_SP_PURCHASE_SET_SUPPLIER_ID_FOR_ITEMS @curID
declare @sql varchar(2000)
set @sql = 'exec A_SP_PURCHASE_UPDATE_FORECAST_FUNNEL_FOR_PURCHASE_ITEMS ''' + @myID + ''',''' + @strNTLogin + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin
set @sql = 'exec A_SP_PURCHASE_AUTO_FILL_IF_FUTURE_FILL_READY ''' + @myRoot + ''',''' + @strNTLogin + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin



fin:





