





CREATE  PROCEDURE A_SP_OBJECT_GET_APPROVAL_ACTIVITY 
	@ret nvarchar(50) OUTPUT,
	@objID nvarchar(50)
AS
declare @id as nvarchar(50)
declare @table as nvarchar(50)

select @id = OBJ_ID, @table = OBJ_TABLE FROM A_OBJECTS WHERE ID = @objID

exec A_SP_GET_APPROVAL_ACTIVITY_BY_TABLE_AND_ID
	@ret OUTPUT,@table ,@id







