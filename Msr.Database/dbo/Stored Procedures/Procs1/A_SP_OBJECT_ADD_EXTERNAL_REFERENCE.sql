CREATE PROCEDURE dbo.A_SP_OBJECT_ADD_EXTERNAL_REFERENCE
@histID varchar(50),
@RootID varchar(50),
@table varchar(2000),
@extID varchar(50),
@strNTLogin varchar(50)
AS
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin

if not exists (SELECT ID FROM A_OBJECT_EXTERNAL_REF WHERE 
	HISTORY_ID = @histID AND
	ROOT_OBJ_ID = @RootID AND
	EXTERNAL_REF_ID = @rootCo + '___' + @extID AND
	ITEM_TABLE = @table)
begin
	INSERT INTO A_OBJECT_EXTERNAL_REF 
		(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,DRCM,MODBY)
	VALUES
		(newID(),@histID,@RootID,@table,@rootCo + '___' + @extID,getDAte(),@strNTLogin)
end

