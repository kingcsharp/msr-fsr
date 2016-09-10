



CREATE     PROCEDURE dbo.A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
@intRootID varchar(50) OUTPUT,
@intHistID varchar(50) OUTPUT,
@intObjID varchar(50) OUTPUT,
@extID varchar(50),
@objTable varchar(50),
@strNTLogin varchar(50)
AS
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
declare @newExtID varchar(100)
set @newExtID = @rootCo + '___' + @extID
print @newExtID



SELECT @intRootID = ROOT_OBJ_ID
	FROM A_OBJECT_EXTERNAL_REF 
	WHERE EXTERNAL_REF_ID = @newExtID
	and ITEM_TABLE = @objTable

if @intRootID is not null
	begin
	SELECT @intHistID = OBJ_ID, @intObjID = ID FROM 
		A_OBJECTS WHERE ROOT = @intRootID AND STATUS LIKE 'APPROVED%'
	if @intHistID is null
		begin
		print 'THIS EXTERNAL REFERENCE HAS BEEN DELETED'
		DELETE FROM A_OBJECT_EXTERNAL_REF WHERE EXTERNAL_REF_ID = @newExtID
	and ITEM_TABLE = @objTable
		SET @intRootID = null
		end
	end









