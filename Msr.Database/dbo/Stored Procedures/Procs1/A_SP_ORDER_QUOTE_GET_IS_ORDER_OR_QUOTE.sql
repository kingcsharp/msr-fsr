CREATE PROCEDURE A_SP_ORDER_QUOTE_GET_IS_ORDER_OR_QUOTE
@objID varchar(50)
AS
declare @strTable varchar(50)
SELECT @strTable = OBJ_TABLE FROM A_OBJECTS WHERE ID = @objID
return @strTable
