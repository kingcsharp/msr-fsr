
CREATE  PROCEDURE DBO.A_SP_FILL_GET_DATA_FOR_FILL_ITEM
@fillItemID varchar(50),
@strNTLogin varchar(50)
AS
declare @fillType varchar(50)
SELECT @fillType = OBJ_TABLE FROM A_V_APPROVED_OBJECTS WHERE ID = @fillItemID

if @fillType = 'A_ACTUAL_PARTS_HISTORY'
	begin
	SELECT *, isNull(NAME,'') + '[' + convert(varchar(50),isNull(QTY,1)) + ']' FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @fillItemID
	end


