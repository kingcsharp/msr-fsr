








CREATE     PROCEDURE A_SP_OBJECT_GET_MAIN_DATA_BY_TABLE
	@strTABLE nvarchar(50),
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @myObjID as nvarchar(50)
if @strTable = 'A_PEOPLE'
	SELECT @myObjID = OBJECT_ID FROM A_APPROVED_PEOPLE WHERE ID = @strID

exec A_SP_OBJECT_GET_MAIN_DATA @myObjID,@strNTLogin












